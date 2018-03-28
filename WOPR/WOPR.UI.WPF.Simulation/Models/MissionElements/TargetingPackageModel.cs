using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;

namespace WOPR.UI.WPF.Simulation.Models.MissionElements
{
    public class TargetingPackageModel : ModelBase<TargetingPackageDO>, ITargetingPackageModel
    {
        private ObservableCollection<TargetingPackageElementModel> m_Elements;

        public ObservableCollection<TargetingPackageElementModel> Elements
        {
            get
            {
                return m_Elements;
            }
            private set
            {
                m_Elements = value;
                OnPropertyChanged("Elements");
            }
        }

        public TargetingPackageModel(TargetingPackageDO package)
            : base(package)
        {
            if (package.Elements == null || package.Elements.Count == 0)
            {
                throw new InvalidOperationException("The package contains no elements");
            }

            Initialise(package);
        }

        private void Initialise(TargetingPackageDO package)
        {
            this.Elements = new ObservableCollection<TargetingPackageElementModel>();
            foreach (var element in package.Elements)
            {
                TargetingPackageElementModel elementModel = new TargetingPackageElementModel(element);
                this.Elements.Add(elementModel);
            }
        }
    }

    public interface ITargetingPackageModel : IModel<TargetingPackageDO>
    {
        ObservableCollection<TargetingPackageElementModel> Elements { get; }
    }
}
