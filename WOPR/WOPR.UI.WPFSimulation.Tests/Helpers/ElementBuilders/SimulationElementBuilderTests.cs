using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Helpers.ElementBuilders;
using WOPR.UI.WPF.Simulation.Models;
using WOPR.UI.WPF.Simulation.Models.Platforms;
using WOPR.UI.WPF.Simulation.Models.Platforms.Offensive;

namespace WOPR.UI.WPFSimulation.Tests.Models
{
    [TestFixture]
    public class SimulationElementBuilderTests
    {
        private ISimulationElementBuilder builder;
        Mock<IPlatformModelFactory> mockPlatformFactory;
        Mock<IAssetBuilder> mockAssetBuilder;

        [SetUp]
        public void SetUp()
        {
            Mock<IMessageBus> mockMessageBus = new Mock<IMessageBus>();
            mockPlatformFactory = new Mock<IPlatformModelFactory>();
            mockAssetBuilder = new Mock<IAssetBuilder>();
            builder = new SimulationElementBuilder(mockMessageBus.Object, mockPlatformFactory.Object, mockAssetBuilder.Object);
        }

        [Test]
        public void SimulationElementBuilder_UT001_BuildController_WhenPassedNullController_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => builder.BuildController(null)));
        }

        [Test]
        public void SimulationElementBuilder_UT002_BuildController_WhenPassedValidController_ReturnsAControllerModel()
        {
            ActorDO actor = new ActorDO();
            ControllerDO controller = new ControllerDO(actor);

            IControllerModel model = builder.BuildController(controller);

            Assert.IsNotNull(model);
        }

        [Test]
        public void SimulationElementBuilder_UT003_BuildCommanders_WhenPassedNullCommanderCollection_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => builder.BuildCommanders(null)));
        }

        [Test]
        public void SimulationElementBuilder_UT004_BuildCommanders_WhenPassed5Commanders_Returns5CommanderModels()
        {
            List<CommanderDO> commanders = new List<CommanderDO>();
            var platformMock = Mock.Of<IPlatformModel>();
            mockPlatformFactory.Setup<IPlatformModel>(x => x.GetPlatformModel(It.IsAny<PlatformBaseDO>(), It.IsAny<IMessageBus>())).Returns(platformMock);
            for (int i = 0; i < 5; i++)
            {
                commanders.Add(new CommanderDO { Platform = new DummyPlatform() });
            }

            var models = builder.BuildCommanders(commanders);

            Assert.IsNotNull(models);
            Assert.IsTrue(models.Count() == 5);
            // Make sure each commander do has a corresponding commander model in the collection
            Assert.IsTrue(commanders.All(x => models.Any(y => y.Entity == x)));
        }

        [Test]
        public void SimulationElementBuilder_UT005_BuildCommanders_WhenPassedFiveSiloCommanders_ReturnsFiveSiloCommanderModelsContainingASiloModel()
        {
            List<CommanderDO> commanders = new List<CommanderDO>();
            var platformMock = Mock.Of<IPlatformModel>();
            mockPlatformFactory.Setup<IPlatformModel>(x => x.GetPlatformModel(It.IsAny<PlatformBaseDO>(), It.IsAny<IMessageBus>())).Returns(platformMock);
            for (int i = 0; i < 5; i++)
            {
                SiloDO silo = new SiloDO();
                CommanderDO commander = new CommanderDO();
                commander.Platform = silo;
                commanders.Add(commander);
            }

            var models = builder.BuildCommanders(commanders);

            Assert.IsNotNull(models);
            Assert.IsTrue(models.Count() == 5);
            // Make sure each model has a platform set
            Assert.IsTrue(models.All(x => x.Platform != null));
        }

        //[Test]
        //public void SimulationElementBuilder_UT005_BuildOffensivePlatforms_WhenPassedNullAssetsCollection_ThrowsArgumentNullException()
        //{
        //    Assert.Throws<ArgumentNullException>(new TestDelegate(() => builder.BuildOffensivePlatforms(null)));
        //}

        //[Test]
        //public void SimulationElementBuilder_UT006_BuildOffensivePlatforms_WhenPassedFiveSilos_ReturnsFiveSiloModels()
        //{
        //    List<SiloDO> silos = new List<SiloDO>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        silos.Add(new SiloDO());
        //    }

        //    var siloModels = builder.BuildOffensivePlatforms(silos);

        //    Assert.IsNotNull(siloModels);
        //    Assert.IsTrue(siloModels.Count() == 5);
            
        //}
    }

    public class DummyPlatform : PlatformBaseDO
    {

    }
}
