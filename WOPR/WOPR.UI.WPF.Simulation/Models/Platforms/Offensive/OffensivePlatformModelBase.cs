using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Models.MissionElements;

namespace WOPR.UI.WPF.Simulation.Models.Platforms.Offensive
{
    public class OffensivePlatformModelBase<TPlatform> : ModelBase<TPlatform>, IOffensivePlatformModel<TPlatform>
        where TPlatform : OffensivePlatformBaseDO
    {
        private ITargetingPackageModel m_TargetingPackage;

        public ITargetingPackageModel TargetingPackage
        {
            get
            {
                return m_TargetingPackage;
            }
            set
            {
                m_TargetingPackage = value;
                OnPropertyChanged("TargetingPackage");
            }
        }

        public OffensivePlatformModelBase(TPlatform platform)
            : base(platform)
        {
        }

        public void Initialise(ITargetingPackageModel targetingPackage)
        {
            if(targetingPackage == null)
            {
                throw new ArgumentNullException("The targeting package cannot be null");
            }
            TargetingPackage = targetingPackage;
        }
    }

    public interface IOffensivePlatformModel<TPlatform> : IModel<TPlatform>, IPlatformModel
        where TPlatform : OffensivePlatformBaseDO
    {
        ITargetingPackageModel TargetingPackage { get; }

        void Initialise(ITargetingPackageModel targetingPackage);
    }
}
