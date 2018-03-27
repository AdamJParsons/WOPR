using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Messages;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Models;
using WOPR.UI.WPF.Simulation.Models.Platforms;

namespace WOPR.UI.WPFSimulation.Tests.Models.CommandControl
{
    [TestFixture]
    public class CommanderModelTests
    {
        [Test]
        public void CommanderModel_UT001_InitialisesInStandbyState()
        {
            // Given a commander model...
            CommanderDO commander = new CommanderDO();
            IPlatformModel mockPlatform = Mock.Of<IPlatformModel>();
            Mock<IMessageBus> mockMessageBus = new Mock<IMessageBus>();

            ICommanderModel model = new CommanderModel(commander, mockPlatform, mockMessageBus.Object);

            // The commander acknowledges reciept of the message
            Assert.IsTrue(model.Status == CommanderStatus.Standby);
        }

        [Test]
        public void CommanderModel_UT002_OnEAMRecieved_SetsStatusToEAMRecieved()
        {
            // Given a commander model...
            CommanderDO commander = new CommanderDO();
            IPlatformModel mockPlatform = Mock.Of<IPlatformModel>();
            MessageBus mockMessageBus = new MessageBus();

            ICommanderModel model = new CommanderModel(commander, mockPlatform, mockMessageBus);

            // When an EAM is sent
            EmergencyActionMessage eam = new EmergencyActionMessage();
            mockMessageBus.Send<EmergencyActionMessage>(eam);

            // The commander acknowledges reciept of the message
            Assert.IsTrue(model.Status == CommanderStatus.EAMRecieved);
        }

        [Test]
        public void CommanderModel_UT002_OnValidEAMRecieved_PlatformIsInitialisedWithAppropriateTargetingPackage()
        {
            // Given a commander model...
            CommanderDO commander = new CommanderDO();
            IPlatformModel mockPlatform = Mock.Of<IPlatformModel>();
            MessageBus mockMessageBus = new MessageBus();

            ICommanderModel model = new CommanderModel(commander, mockPlatform, mockMessageBus);

            // When a valid EAM is sent
            EmergencyActionMessage eam = new EmergencyActionMessage();
            mockMessageBus.Send<EmergencyActionMessage>(eam);

            // The commander initialises their platform with the correct package
            
        }
    }


}
