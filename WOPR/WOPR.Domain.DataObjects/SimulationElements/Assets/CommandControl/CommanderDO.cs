using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms;

namespace WOPR.Domain.DataObjects.SimulationElements.Assets
{
    public class CommanderDO : DynamicAssetBaseDO
    {
        public PlatformBaseDO Platform { get; set; }

    }
}
