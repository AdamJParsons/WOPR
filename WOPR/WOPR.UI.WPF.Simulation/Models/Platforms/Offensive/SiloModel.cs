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
    public class SiloModel : OffensivePlatformModelBase<SiloDO, IntercontinentalBallisticMissileDO>, IOffensivePlatformModel<SiloDO, IntercontinentalBallisticMissileDO>
    {
        public SiloModel(SiloDO silo)
            : base(silo)
        {
        }

        protected override double InitialisationOffset
        {
            get { return 0; }
        }
    }
}
