using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Domain.DataObjects.SimulationElements
{
    public class StrategyDO : BaseDO
    {
        public ActorDO Role { get; set; }

        public string Name { get; set; }

        public ActorDO TargetRole { get; set; }
    }
}
