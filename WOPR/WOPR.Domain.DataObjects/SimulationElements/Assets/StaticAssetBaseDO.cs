using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Domain.DataObjects.SimulationElements.Assets
{
    public abstract class StaticAssetBaseDO : AssetBaseDO
    {
        public LocationDO Location { get; set; }
    }
}
