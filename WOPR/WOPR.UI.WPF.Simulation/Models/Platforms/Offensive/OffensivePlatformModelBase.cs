using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Helpers.ElementBuilders;
using WOPR.UI.WPF.Simulation.Models.MissionElements;
using WOPR.UI.WPF.Simulation.Models.MissionElements.StrategicAssets;

namespace WOPR.UI.WPF.Simulation.Models.Platforms.Offensive
{
    public abstract class OffensivePlatformModelBase<TPlatform, TMunition> : ModelBase<TPlatform>, IOffensivePlatformModel<TPlatform, TMunition>
        where TPlatform : OffensivePlatformBaseDO
        where TMunition : StrategicAssetBaseDO
    {
        private ITargetingPackageModel m_TargetingPackage;

        private PlatformStatus m_Status;

        private ObservableCollection<IAsset> m_Assets;

        public ITargetingPackageModel TargetingPackage
        {
            get
            {
                return m_TargetingPackage;
            }
            private set
            {
                m_TargetingPackage = value;
                OnPropertyChanged("TargetingPackage");
            }
        }

        public PlatformStatus Status
        {
            get
            {
                return m_Status;
            }
            private set
            {
                m_Status = value;
                OnPropertyChanged("Status");
            }
        }

        public ObservableCollection<IAsset> Assets
        {
            get
            {
                return m_Assets;
            }
            private set
            {
                m_Assets = value;
                OnPropertyChanged("Assets");
            }
        }

        protected abstract double InitialisationOffset { get; }

        public OffensivePlatformModelBase(TPlatform platform)
            : base(platform)
        {
            this.Status = PlatformStatus.Idle;
            this.Assets = new ObservableCollection<IAsset>();
        }

        public async Task Initialise(ITargetingPackageModel targetingPackage)
        {
            if(targetingPackage == null)
            {
                throw new ArgumentNullException("The targeting package cannot be null");
            }
            if (targetingPackage.Elements.Count > this.Assets.Count)
            {
                throw new InvalidOperationException("The targeting package contains more elements than we can handle");
            }

            this.Status = PlatformStatus.Initialising;

            TargetingPackage = targetingPackage;

            AssignTargets(targetingPackage);

            // wait until the initialisation time has elapsed before we actually initialise
            await Task.Delay(TimeSpan.FromSeconds(InitialisationOffset)).ContinueWith((t) => OnInitialisationStart());
        }

        private void AssignTargets(ITargetingPackageModel targetPackage)
        {
            List<IAsset> assets = this.Assets.ToList();
            foreach (var element in targetPackage.Elements)
            {
                // for each target in the package, assign an appropriate asset
                IAsset toAssign = assets.ElementAt(0);
                ((IStrategicAsset)toAssign).Target = element.Location;
                assets.Remove(toAssign);
            }
        }

        /// <summary>
        /// Once the platform is spun up, we can actually start applying the target package
        /// </summary>
        private void OnInitialisationStart()
        {
            this.Status = PlatformStatus.Initialised;

            foreach (var asset in Assets.Cast<IStrategicAsset>())
            {
                if (asset.Target != null)
                {
                    asset.Launch();
                }
            }
        }
    }

    public enum PlatformStatus
    {
        NOTSET = 0,

        Idle = 1,

        Initialising = 2,

        Initialised = 3
    }

    public interface IOffensivePlatformModel<TPlatform, TMunition> : IModel<TPlatform>, IPlatformModel
        where TPlatform : OffensivePlatformBaseDO
        where TMunition : StrategicAssetBaseDO
    {
        ITargetingPackageModel TargetingPackage { get; }

        Task Initialise(ITargetingPackageModel targetingPackage);
    }
}
