using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Helpers.ElementBuilders;

namespace WOPR.UI.WPF.Simulation.Models.Platforms.Offensive
{
    public class StrategicBomberModel : OffensivePlatformModelBase<StrategicBomberDO, GravityBombDO>, IOffensivePlatformModel<StrategicBomberDO, GravityBombDO>
    {
        public StrategicBomberModel(StrategicBomberDO submarine)
            : base(submarine)
        {
        }

        protected override double InitialisationOffset
        {
            get
            {
                return 0;
            }
        }
    }
}
