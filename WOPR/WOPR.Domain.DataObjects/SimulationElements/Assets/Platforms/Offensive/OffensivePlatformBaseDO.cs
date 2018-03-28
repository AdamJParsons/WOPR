using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;

namespace WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive
{
    public abstract class OffensivePlatformBaseDO : PlatformBaseDO
    {
        public List<StrategicAssetBaseDO> Assets { get; set; }
    }
}
