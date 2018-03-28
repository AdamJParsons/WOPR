using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Models;
using WOPR.UI.WPF.Simulation.Models.MissionElements;
using WOPR.UI.WPF.Simulation.Models.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Simulation.Models.Platforms;
using WOPR.UI.WPF.Simulation.Models.Platforms.Offensive;

namespace WOPR.UI.WPFSimulation.Tests.Models.Platforms
{
    [TestFixture]
    public class OffensivePlatformModelBaseTests
    {
        private DummyOffensivePlatformModel GetModel(int initTime = 0)
        {
            List<StrategicAssetBaseDO> dummyAssets = new List<StrategicAssetBaseDO>() { new DummyAsset() };
            DummyPlatform platform = new DummyPlatform(dummyAssets);

            return new DummyOffensivePlatformModel(platform);
        }

        [Test]
        public void OffensivePlatformModelBase_UT001_PlatformIsCreatedWithIdleStatus()
        {
            var model = GetModel();
            Assert.IsTrue(model.Status == PlatformStatus.Idle);
        }

        [Test]
        public void OffensivePlatformModelBase_UT002_Initialise_WhenPassedNullTargetingPackage_ThrownsArgumentNullException()
        {
            var model = GetModel();
            Assert.ThrowsAsync<ArgumentNullException>(async () => await model.Initialise(null));
        }

        [Test]
        public void OffensivePlatformModelBase_UT003_Initialise_WhenPassedTargetingPackage_SetsTargetingPackageProperty()
        {
            var model = GetModel();
            TargetingPackageDO package = GetTargetingPackage();
            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            model.Initialise(packageModel);

            Assert.IsNotNull(model.TargetingPackage);
        }

        [Test]
        public async Task OffensivePlatformModelBase_UT004_Initialise_WhenPassedTargetingPackage_SetsStatusToInitialisedWhenComplete()
        {
            var model = GetModel(1);
            TargetingPackageDO package = GetTargetingPackage();
            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            await model.Initialise(packageModel);

            Assert.IsTrue(model.Status == PlatformStatus.Initialised);
        }

        [Test]
        public async Task OffensivePlatformModelBase_UT005_Initialise_WhenPassedTargetingPackage_AssignsTargets()
        {
            var model = GetModel();
            TargetingPackageDO package = GetTargetingPackage();

            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            await model.Initialise(packageModel);

            Assert.IsTrue(model.Assets.Count > 0);
            Assert.IsTrue(model.Assets.All(x => ((IStrategicAsset)x).Target != null));
        }

        private static TargetingPackageDO GetTargetingPackage()
        {
            TargetingPackageDO package = GetTargetingPackage();
            package.Elements = new List<TargetingPackageElementDO>();
            package.Elements.Add(new TargetingPackageElementDO { Target = new LocationDO() });
            return package;
        }

        [Test]
        public async Task OffensivePlatformModelBase_UT006_WhenInitialisationComplete_AssetsWithTargetAssignedAreLaunched()
        {
            var model = GetModel();

            Mock<IStrategicAsset> mockAsset = new Mock<IStrategicAsset>();
            ILocationModel mockLocation = Mock.Of<ILocationModel>();
            mockAsset.SetupGet<ILocationModel>(x => x.Target).Returns(mockLocation);

            model.Assets.Clear();
            model.Assets.Add(mockAsset.Object);

            TargetingPackageDO package = GetTargetingPackage();
            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            await model.Initialise(packageModel);

            mockAsset.Verify(x => x.Launch(), Times.Once);
        }

        [Test]
        public async Task OffensivePlatformModelBase_UT007_WhenInitialisationComplete_AssetsWithNoTargetAssignedAreNotLaunched()
        {
            var model = GetModel();

            Mock<IStrategicAsset> mockAsset = new Mock<IStrategicAsset>();

            model.Assets.Clear();
            model.Assets.Add(mockAsset.Object);

            TargetingPackageDO package = GetTargetingPackage();
            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            await model.Initialise(packageModel);

            mockAsset.Verify(x => x.Launch(), Times.Never);
        }

        [Test]
        public void OffensivePlatformModelBase_UT008_WhenPassedTargetingPackageWithMoreElementsThenPlatformAssets_InvalidOperationExceptionIsThrown()
        {
            // Given a model with one asset...
            var model = GetModel();
            Mock<IStrategicAsset> mockAsset = new Mock<IStrategicAsset>();
            model.Assets.Clear();
            model.Assets.Add(mockAsset.Object);

            // and 5 package elements...
            TargetingPackageDO package = new TargetingPackageDO();
            package.Elements = new List<TargetingPackageElementDO>();
            for (int i = 0; i < 5; i++)
            {
                package.Elements.Add(new TargetingPackageElementDO());
            }
            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await model.Initialise(packageModel));
        }

        public class DummyPlatform : OffensivePlatformBaseDO
        {
            public DummyPlatform(List<StrategicAssetBaseDO> dummyAssets)
            {
                base.Assets = dummyAssets;
            }
        }

        public class DummyAsset : StrategicAssetBaseDO
        {
            public DummyAsset()
            {
                
            }
        }

        public class DummyOffensivePlatformModel : OffensivePlatformModelBase<DummyPlatform, DummyAsset>
        {
            private double initTime;

            public DummyOffensivePlatformModel(DummyPlatform platform, double initialisationTime = 0) : base(platform)
            {
                initTime = initialisationTime;
            }

            protected override double InitialisationOffset
            {
                get { return initTime; }
            }
        }
    }
}
