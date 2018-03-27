using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Models.MissionElements;
using WOPR.UI.WPF.Simulation.Models.Platforms;
using WOPR.UI.WPF.Simulation.Models.Platforms.Offensive;

namespace WOPR.UI.WPFSimulation.Tests.Models.Platforms
{
    [TestFixture]
    public class OffensivePlatformModelBaseTests
    {
        [Test]
        public void OffensivePlatformModelBase_UT001_Initialise_WhenPassedNullTargetingPackage_ThrownsArgumentNullException()
        {
            DummyPlatform platform = new DummyPlatform();
            DummyOffensivePlatformModel model = new DummyOffensivePlatformModel(platform);

            Assert.Throws<ArgumentNullException>(new TestDelegate(() => model.Initialise(null)));
        }

        [Test]
        public void OffensivePlatformModelBase_UT002_Initialise_WhenPassedTargetingPackage_SetsTargetingPackageProperty()
        {
            DummyPlatform platform = new DummyPlatform();
            DummyOffensivePlatformModel model = new DummyOffensivePlatformModel(platform);
            TargetingPackageDO package = new TargetingPackageDO();
            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            model.Initialise(packageModel);

            Assert.IsNotNull(model.TargetingPackage);
        }

        public class DummyPlatform : OffensivePlatformBaseDO
        {

        }

        public class DummyOffensivePlatformModel : OffensivePlatformModelBase<DummyPlatform>
        {
            public DummyOffensivePlatformModel(DummyPlatform platform) : base(platform)
            {
            }
        }
    }
}
