using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Messages;

namespace WOPR.UI.WPF.Common.Messages
{
    public class MessageBus : IMessageBus
    {
        private Dictionary<Type, List<ActionHandlerBase>> m_MessageHandlers = new Dictionary<Type, List<ActionHandlerBase>>();

        public ReadOnlyCollection<SubscriptionToken> Subscriptions
        {
            get
            {
                return m_MessageHandlers.SelectMany(x => x.Value).Select(x => x.Token).ToList().AsReadOnly();
            }
        }

        public SubscriptionToken Subscribe<TMessage>(Action<TMessage> toPerform) where TMessage : IMessage
        {
            ActionHandler<TMessage> handler = new ActionHandler<TMessage>(toPerform);
            if (!this.m_MessageHandlers.ContainsKey(typeof(TMessage)))
            {
                this.m_MessageHandlers.Add(typeof(TMessage), new List<ActionHandlerBase>());
            }
            this.m_MessageHandlers[typeof(TMessage)].Add(handler);
            return handler.Token;
            // TODO : Multiple subscribe?
        }

        public void Unsubscribe<TMessage>(SubscriptionToken token) where TMessage : IMessage
        {
            var entry = this.m_MessageHandlers[typeof(TMessage)];
            var handler = entry.SingleOrDefault(x => x.Token == token);
            if (handler != null)
            {
                entry.Remove(handler);
            }
            // TODO : Key not found
        }


        public void Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            // Get the handlers that can handle the message
            List<ActionHandlerBase> handlers;
            if (this.m_MessageHandlers.TryGetValue(typeof(TMessage), out handlers))
            {
                foreach (var handler in handlers.Cast<ActionHandler<TMessage>>())
                {
                    handler.Action(message);
                }
            }
            // TODO : What if not found
        }

        public void SendAsync<TMessage>(TMessage message) where TMessage : IMessage
        {
            // Get the handlers that can handle the message
            List<ActionHandlerBase> handlers;
            if (this.m_MessageHandlers.TryGetValue(typeof(TMessage), out handlers))
            {
                Task.WaitAll(handlers.Cast<ActionHandler<TMessage>>().Select(x => Task.Run(() =>  x.Action(message), CancellationToken.None)).ToArray());
            }
            // TODO : What if not found
        }

        private class ActionHandlerBase
        {
            public SubscriptionToken Token { get; private set; }

            public ActionHandlerBase()
            {
                this.Token = new SubscriptionToken();
            }
        }

        private class ActionHandler<TMessage> : ActionHandlerBase
            where TMessage : IMessage
        {
            public Type MessageType
            {
                get
                {
                    return typeof(TMessage);
                }
            }

            public Action<TMessage> Action { get; private set; }

            public ActionHandler(Action<TMessage> toPerform)
            {
                this.Action = toPerform;

            }
        }
    }

    public interface IMessageBus
    {
        ReadOnlyCollection<SubscriptionToken> Subscriptions { get; }

        SubscriptionToken Subscribe<TMessage>(Action<TMessage> toPerform)
            where TMessage : IMessage;

        void Unsubscribe<TMessage>(SubscriptionToken token)
            where TMessage : IMessage;

        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;

        void SendAsync<TMessage>(TMessage message)
            where TMessage : IMessage;
    }

    public class SubscriptionToken
    {
        public Guid Identifier { get; private set; }

        public SubscriptionToken()
        {
            Identifier = Guid.NewGuid();
        }
    }
}
