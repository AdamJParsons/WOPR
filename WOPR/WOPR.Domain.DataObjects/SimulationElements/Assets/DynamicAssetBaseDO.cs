using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Domain.DataObjects.SimulationElements.Assets
{
    public abstract class DynamicAssetBaseDO : AssetBaseDO
    {
        public LocationDO HomeLocation { get; set; }

        public Position CurrentPosition { get; set; }
    }
}
