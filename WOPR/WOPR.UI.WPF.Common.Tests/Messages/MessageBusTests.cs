using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Messages;
using WOPR.UI.WPF.Common.Messages;

namespace WOPR.UI.WPF.Common.Tests.Messages
{
    [TestFixture]
    public class MessageBusTests
    {
        [Test]
        public void MessageBus_UT001_Subscribe_AddsSubscriberToSubscribers()
        {
            IMessageBus messageBus = new MessageBus();

            DummyMessage message = new DummyMessage();

            Mock<Action<DummyMessage>> mockAction = new Mock<Action<DummyMessage>>();

            var token = messageBus.Subscribe<DummyMessage>(mockAction.Object);

            Assert.Contains(token, messageBus.Subscriptions);
        }

        [Test]
        public void MessageBus_UT002_Unsubscribe_RemovesSubscriberFromSubscribers()
        {
            IMessageBus messageBus = new MessageBus();

            Mock<Action<DummyMessage>> mockAction = new Mock<Action<DummyMessage>>();
            var token = messageBus.Subscribe<DummyMessage>(mockAction.Object);

            messageBus.Unsubscribe<DummyMessage>(token);

            Assert.IsFalse(messageBus.Subscriptions.Contains(token));
        }

        [Test]
        public void MessageBus_UT003_Send_SendsMessageToSubscribers()
        {
            IMessageBus messageBus = new MessageBus();

            DummyMessage message = new DummyMessage();

            Mock<Action<IMessage>> mockAction = new Mock<Action<IMessage>>();

            messageBus.Subscribe<DummyMessage>(mockAction.Object);

            messageBus.Send<DummyMessage>(message);

            mockAction.Verify(x => x(message), Times.Once);
        }

        [Test]
        public void MessageBus_UT004_Send_SendsSameMessageToTwoSubscribers()
        {
            IMessageBus messageBus = new MessageBus();

            DummyMessage message = new DummyMessage();

            Mock<Action<DummyMessage>> mockFirstAction = new Mock<Action<DummyMessage>>();
            Mock<Action<DummyMessage>> mockSecondAction = new Mock<Action<DummyMessage>>();

            messageBus.Subscribe<DummyMessage>(mockFirstAction.Object);
            messageBus.Subscribe<DummyMessage>(mockSecondAction.Object);

            messageBus.Send<DummyMessage>(message);

            mockFirstAction.Verify(x => x(message), Times.Once);
            mockSecondAction.Verify(x => x(message), Times.Once);
        }

        [Test]
        public void MessageBus_UT005_Send_WhenTwoDifferentSubscriptions_OnlyOneSubscriberIsNotified()
        {
            IMessageBus messageBus = new MessageBus();

            DummyMessage message = new DummyMessage();
            AlternateDummyMessage alternateMessage = new AlternateDummyMessage();
            Mock<Action<DummyMessage>> mockFirstAction = new Mock<Action<DummyMessage>>();
            Mock<Action<AlternateDummyMessage>> mockSecondAction = new Mock<Action<AlternateDummyMessage>>();

            messageBus.Subscribe<DummyMessage>(mockFirstAction.Object);
            messageBus.Subscribe<AlternateDummyMessage>(mockSecondAction.Object);

            messageBus.Send<DummyMessage>(message);

            mockFirstAction.Verify(x => x(message), Times.Once);
            mockSecondAction.Verify(x => x(alternateMessage), Times.Never);
        }

        [Test]
        public void MessageBus_UT006_Send_SendsMessageTo1000Subscribers()
        {
            IMessageBus messageBus = new MessageBus();

            DummyMessage message = new DummyMessage();

            List<Mock<Action<IMessage>>> verifiableActions = new List<Mock<Action<IMessage>>>();
            for (int i = 0; i < 1000; i++)
            {
                Mock<Action<IMessage>> mockAction = new Mock<Action<IMessage>>();

                messageBus.Subscribe<DummyMessage>(mockAction.Object);
                verifiableActions.Add(mockAction);
            }

            messageBus.Send<DummyMessage>(message);
            foreach (var action in verifiableActions)
            {
                action.Verify(x => x(message), Times.Once);
            }
            
        }

        [Test]
        public void MessageBus_UT007_SendAsync_SendsMessageTo1000Subscribers()
        {
            IMessageBus messageBus = new MessageBus();

            DummyMessage message = new DummyMessage();

            List<Mock<Action<IMessage>>> verifiableActions = new List<Mock<Action<IMessage>>>();
            for (int i = 0; i < 1000; i++)
            {
                Mock<Action<IMessage>> mockAction = new Mock<Action<IMessage>>();

                messageBus.Subscribe<DummyMessage>(mockAction.Object);
                verifiableActions.Add(mockAction);
            }

            messageBus.SendAsync<DummyMessage>(message);
            foreach (var action in verifiableActions)
            {
                action.Verify(x => x(message), Times.Once);
            }

        }

        public class DummyMessage : MessageBase
        {
        }

        public class AlternateDummyMessage : MessageBase
        {
        }
    }
}
