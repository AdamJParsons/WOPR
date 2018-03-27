using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;

namespace WOPR.UI.WPF.Simulation.Models.MissionElements
{
    public class TargetingPackageModel : ModelBase<TargetingPackageDO>, ITargetingPackageModel
    {
        public TargetingPackageModel(TargetingPackageDO package) : base(package)
        {
        }
    }

    public interface ITargetingPackageModel : IModel<TargetingPackageDO>
    {

    }
}
