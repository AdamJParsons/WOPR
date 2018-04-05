using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Simulation.Models.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Simulation.Models.Platforms;

namespace WOPR.UI.WPF.Simulation.Helpers.ElementBuilders
{
    public class StrategicAssetBuilder : IStrategicAssetBuilder
    {
        public IStrategicAsset BuildAsset(StrategicAssetBaseDO asset)
        {
            if (asset == null)
            {
                throw new ArgumentNullException("The asset cannot be null");
            }

            if (asset is IntercontinentalBallisticMissileDO)
            {
                return new IntercontinentalBallisticMissileModel(asset as IntercontinentalBallisticMissileDO);
            }

            // We haven't got any idea what this type of asset is.
            throw new NotSupportedException();
        }
    }

    public interface IStrategicAssetBuilder
    {
        IStrategicAsset BuildAsset(StrategicAssetBaseDO asset);
    }

    public class AssetBuilder : IAssetBuilder
    {
        private readonly IStrategicAssetBuilder m_StrategicAssetBuilder;

        public AssetBuilder(IStrategicAssetBuilder strategicAssetBuilder)
        {
            m_StrategicAssetBuilder = strategicAssetBuilder;
        }

        public IAsset BuildAsset(AssetBaseDO asset)
        {
            if (asset is StrategicAssetBaseDO)
            {
                return m_StrategicAssetBuilder.BuildAsset(asset as StrategicAssetBaseDO);
            }

            return null;
        }
    }

    public interface IAssetBuilder
    {
        IAsset BuildAsset(AssetBaseDO asset);
    }
}
