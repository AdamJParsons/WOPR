using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Messages;
using WOPR.UI.WPF.Common.Messages;

namespace WOPR.UI.WPF.Simulation.Models
{
    /// <summary>
    /// Equivalent of the NMCC
    /// </summary>
    public class ControllerModel : ModelBase<ControllerDO>, IControllerModel
    {
        private readonly IMessageBus m_MessageBus;

        public ControllerModel(ControllerDO controller, IMessageBus messageBus)
            : base(controller)
        {
            this.m_MessageBus = messageBus;
        }

        public void ExecuteWarOrder(EmergencyWarOrder order)
        {
            EmergencyActionMessage eam = PrepareEAM(order);
            m_MessageBus.Send<EmergencyActionMessage>(eam);
        }

        private EmergencyActionMessage PrepareEAM(EmergencyWarOrder order)
        {
            return new EmergencyActionMessage();
        }
    }

    public interface IControllerModel : IModel<ControllerDO>
    {
        void ExecuteWarOrder(EmergencyWarOrder order);
    }
}
