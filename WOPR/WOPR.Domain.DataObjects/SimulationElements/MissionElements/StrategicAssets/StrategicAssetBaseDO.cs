using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets;

namespace WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets
{
    public class StrategicAssetBaseDO : AssetBaseDO
    {
        public LocationDO Target { get; set; }
    }
}
