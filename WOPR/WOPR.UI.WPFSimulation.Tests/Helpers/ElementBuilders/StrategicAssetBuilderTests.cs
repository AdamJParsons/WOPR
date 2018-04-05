using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Simulation.Helpers.ElementBuilders;
using WOPR.UI.WPF.Simulation.Models.MissionElements.StrategicAssets;

namespace WOPR.UI.WPFSimulation.Tests.Helpers.ElementBuilders
{
    [TestFixture]
    public class StrategicAssetBuilderTests
    {
        [Test]
        public void StrategicAssetBuilder_UT001_BuildAsset_WhenPassedNullAsset_ThrowsArgumentNullException()
        {
            IStrategicAssetBuilder builder = new StrategicAssetBuilder();
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => builder.BuildAsset(null)));
        }

        [Test]
        public void StrategicAssetBuilder_UT002_BuildAsset_WhenPassedICBMDO_ReturnsSiloModel()
        {
            IStrategicAssetBuilder builder = new StrategicAssetBuilder();

            IntercontinentalBallisticMissileDO icbm = new IntercontinentalBallisticMissileDO();

            var asset = builder.BuildAsset(icbm);

            Assert.IsNotNull(asset);
            Assert.IsAssignableFrom<IntercontinentalBallisticMissileModel>(asset);
        }

        [Test]
        public void StrategicAssetBuilder_UT003_BuildAsset_WhenPassedUnknownAsset_ThrowsNotSupportedException()
        {
            IStrategicAssetBuilder builder = new StrategicAssetBuilder();
            DummyUnknownAsset unknown = new DummyUnknownAsset();
            Assert.Throws<NotSupportedException>(new TestDelegate(() => builder.BuildAsset(unknown)));
        }

        public class DummyUnknownAsset : StrategicAssetBaseDO
        {

        }
    }
}
