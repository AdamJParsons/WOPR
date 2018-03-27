using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;

namespace WOPR.Domain.DataObjects.SimulationElements
{
    public class ActorDO : BaseDO
    {
        public string Name { get; set; }

        public List<CommanderDO> Commanders { get; set; }

        public List<OffensivePlatformBaseDO> OffensivePlatforms { get; set; }
    }
}
