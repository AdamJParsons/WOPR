using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Messages;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Models.Platforms;

namespace WOPR.UI.WPF.Simulation.Models
{
    public class CommanderModel : ModelBase<CommanderDO>, ICommanderModel
    {
        private readonly IMessageBus m_MessageBus;

        private IPlatformModel m_Platform;

        private CommanderStatus m_Status;

        public CommanderStatus Status
        {
            get
            {
                return m_Status;
            }
            private set
            {
                m_Status = value;
                OnPropertyChanged("Status");
            }
        }

        public IPlatformModel Platform
        {
            get
            {
                return m_Platform;
            }
            private set
            {
                m_Platform = value;
                OnPropertyChanged("Platform");
            }
        }

        public CommanderModel(CommanderDO commander, IPlatformModel platform, IMessageBus messageBus)
            : base(commander)
        {
            if(platform == null)
            {
                throw new ArgumentNullException("The platform cannot be null");                
            }
            if (messageBus == null)
            {
                throw new ArgumentNullException("The message bus cannot be null");
            }
            this.m_MessageBus = messageBus;
            Platform = platform;
            this.Status = CommanderStatus.Standby;

            Initialise();
        }

        private void Initialise()
        {
            m_MessageBus.Subscribe<EmergencyActionMessage>((m) => OnEmergencyActionMessageReceived(m));
        }

        private void OnEmergencyActionMessageReceived(EmergencyActionMessage message)
        {
            this.Status = CommanderStatus.EAMRecieved;
        }
    }

    public interface ICommanderModel : IModel<CommanderDO>
    {
        CommanderStatus Status { get; }

        IPlatformModel Platform { get; }
    }

    public enum CommanderStatus
    {
        NOTSET = 0,

        Standby = 1,

        EAMRecieved = 2
    }
}
