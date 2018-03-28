using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Helpers.ElementBuilders;
using WOPR.UI.WPF.Simulation.Models.Platforms.Offensive;

namespace WOPR.UI.WPFSimulation.Tests.Helpers.ElementBuilders
{
    [TestFixture]
    public class PlatformFactoryTests
    {
        [Test]
        public void OffensivePlatformFactory_UT001_GetPlatformModel_WhenPassedNullPlatform_ThrowsArgumentNullException()
        {
            IPlatformModelFactory factory = new PlatformModelFactory();
            Mock<IMessageBus> mockMessageBus = new Mock<IMessageBus>();

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => factory.GetPlatformModel(null, mockMessageBus.Object)));
        }

        [Test]
        public void OffensivePlatformFactory_UT002_GetPlatformModel_WhenPassedNullMessageBus_ThrowsArgumentNullException()
        {
            IPlatformModelFactory factory = new PlatformModelFactory();
            DummyPlatform dummy = new DummyPlatform();
            
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => factory.GetPlatformModel(dummy, null)));
        }

        [Test]
        public void OffensivePlatformFactory_UT003_GetPlatformModel_WhenPassedSiloPlatform_ReturnsASiloPlatformModel()
        {
            IPlatformModelFactory factory = new PlatformModelFactory();
            Mock<IMessageBus> mockMessageBus = new Mock<IMessageBus>();
            SiloDO silo = new SiloDO();

            var siloModel = factory.GetPlatformModel(silo, mockMessageBus.Object);

            Assert.IsNotNull(siloModel);
            Assert.IsAssignableFrom<SiloModel>(siloModel);
        }

        [Test]
        public void OffensivePlatformFactory_UT004_GetPlatformModel_WhenPassedballisticMissileSubmarinePlatform_ReturnsABallisticMissileSubmarinePlatformModel()
        {
            IPlatformModelFactory factory = new PlatformModelFactory();
            Mock<IMessageBus> mockMessageBus = new Mock<IMessageBus>();
            BallisticMissileSubmarineDO sub = new BallisticMissileSubmarineDO();

            var subModel = factory.GetPlatformModel(sub, mockMessageBus.Object);

            Assert.IsNotNull(subModel);
            Assert.IsAssignableFrom<BallisticMissileSubmarineModel>(subModel);
        }

        [Test]
        public void OffensivePlatformFactory_UT005_GetPlatformModel_WhenPassedStrategicBomberPlatform_ReturnsAStrategicBomberPlatformModel()
        {
            IPlatformModelFactory factory = new PlatformModelFactory();
            Mock<IMessageBus> mockMessageBus = new Mock<IMessageBus>();
            StrategicBomberDO sub = new StrategicBomberDO();

            var subModel = factory.GetPlatformModel(sub, mockMessageBus.Object);

            Assert.IsNotNull(subModel);
            Assert.IsAssignableFrom<StrategicBomberModel>(subModel);
        }

        [Test]
        public void OffensivePlatformFactory_UT003_GetPlatformModel_WhenPassedSiloPlatformWithOneAsset_ReturnsASiloPlatformModelWithOneAsset()
        {
            IPlatformModelFactory factory = new PlatformModelFactory();
            Mock<IMessageBus> mockMessageBus = new Mock<IMessageBus>();
            SiloDO silo = new SiloDO();
            silo.Assets = new List<StrategicAssetBaseDO>();
            DummyAsset asset = new DummyAsset();
            silo.Assets.Add(asset);

            var siloModel = (SiloModel)factory.GetPlatformModel(silo, mockMessageBus.Object);

            //Assert.IsNotNull(siloModel.Assets);
            //Assert.IsTrue(siloModel.Assets.Count == 1);
            //var StrategicAsset = siloModel.Assets.SingleOrDefault(x => x)
            //Assert.IsTrue(siloModel.Assets.Contains();
        }
    }

    public class DummyPlatform : OffensivePlatformBaseDO
    {

    }

    public class DummyAsset : StrategicAssetBaseDO
    {

    }
}
