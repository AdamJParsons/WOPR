using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Messages;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Models;

namespace WOPR.UI.WPFSimulation.Tests.Models
{
    [TestFixture]
    public class ControllerModelTests
    {
        [Test]
        public void ControllerModel_UT001_WhenStrategyReceieved_SendsEAM()
        {
            ActorDO actor = new ActorDO();
            ControllerDO controller = new ControllerDO(actor);
            Mock<IMessageBus> messageBus = new Mock<IMessageBus>();
            IControllerModel controllerModel = new ControllerModel(controller, messageBus.Object);

            EmergencyWarOrder order = new EmergencyWarOrder();

            controllerModel.ExecuteWarOrder(order);

            // Verify that an EAM was sent to the message bus
            messageBus.Verify(x => x.Send<EmergencyActionMessage>(It.IsAny<EmergencyActionMessage>()));
        }
    }
}
