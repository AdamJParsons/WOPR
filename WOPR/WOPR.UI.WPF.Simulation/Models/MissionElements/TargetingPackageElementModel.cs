using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;

namespace WOPR.UI.WPF.Simulation.Models.MissionElements
{
    public class TargetingPackageElementModel : ModelBase<TargetingPackageElementDO>
    {
        private ILocationModel m_Location;

        public ILocationModel Location
        {
            get
            {
                return m_Location;
            }
            private set
            {
                m_Location = value;
                OnPropertyChanged("Location");
            }
        }

        public TargetingPackageElementModel(TargetingPackageElementDO element)
            : base(element)
        {
            if (element.Target == null)
            {
                throw new NullReferenceException("The target location cannot be null");
            }
            this.Location = new LocationModel(element.Target);
        }
    }

    public interface ITargetingPackageElementModel : IModel<TargetingPackageElementDO>
    {
        ILocationModel Location { get; }
    }
}
