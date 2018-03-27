using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Domain.DataObjects.SimulationElements.Assets
{
    /// <summary>
    /// Here, we derive from dynamic asset base so we can support the e3 alternate controller
    /// NMCC and Alternate NMCC will essentially be static.
    /// </summary>
    public class ControllerDO : DynamicAssetBaseDO
    {
        public ActorDO Role { get; private set; }

        public ControllerDO(ActorDO role)
        {
            Role = role;
        }
    }
}
