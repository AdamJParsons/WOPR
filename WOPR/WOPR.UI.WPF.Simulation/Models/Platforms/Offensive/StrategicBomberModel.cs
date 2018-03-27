using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.UI.WPF.Common.Messages;

namespace WOPR.UI.WPF.Simulation.Models.Platforms.Offensive
{
    public class StrategicBomberModel : OffensivePlatformModelBase<StrategicBomberDO>, IOffensivePlatformModel<StrategicBomberDO>
    {
        public StrategicBomberModel(StrategicBomberDO submarine)
            : base(submarine)
        {
        }
    }
}
