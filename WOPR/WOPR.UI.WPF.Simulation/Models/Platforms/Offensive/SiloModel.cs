using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.UI.WPF.Common.Messages;

namespace WOPR.UI.WPF.Simulation.Models.Platforms.Offensive
{
    public class SiloModel : OffensivePlatformModelBase<SiloDO>, IOffensivePlatformModel<SiloDO>
    {
        public SiloModel(SiloDO silo)
            : base(silo)
        {
        }
    }
}
