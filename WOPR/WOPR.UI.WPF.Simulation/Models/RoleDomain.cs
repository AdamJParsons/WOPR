using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.UI.WPF.Common.ViewModels;
using WOPR.UI.WPF.Simulation.Models.Platforms;

namespace WOPR.UI.WPF.Simulation.Models
{
    /// <summary>
    /// This object contains all the simulation elements for a given role
    /// </summary>
    public class RoleDomain : ViewModelBase, IRoleDomain
    {
        private readonly ISimulationElementBuilder m_ElementBuilder;

        private IControllerModel m_ControllerModel;

        private ObservableCollection<ICommanderModel> m_Commanders;

        private ObservableCollection<IPlatformModel> m_OffensivePlatforms;

        public event PropertyChangedEventHandler PropertyChanged;

        public IControllerModel Controller
        {
            get
            {
                return m_ControllerModel;
            }
            private set
            {
                m_ControllerModel = value;
                OnPropertyChanged("Controller");
            }
        }

        public ObservableCollection<ICommanderModel> Commanders
        {
            get
            {
                return m_Commanders;
            }
            private set
            {
                this.m_Commanders = value;
                OnPropertyChanged("Commanders");
            }
        }

        public RoleDomain(ISimulationElementBuilder elementBuilder)
        {
            this.m_ElementBuilder = elementBuilder;
        }

        public void SetData(ActorDO role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("The role cannot be null");
            }
            ControllerDO controller = new ControllerDO(role);
            this.Controller = m_ElementBuilder.BuildController(controller);

            var commanders = m_ElementBuilder.BuildCommanders(role.Commanders);
            this.Commanders = new ObservableCollection<ICommanderModel>(commanders);
        }
    }

    public interface IRoleDomain : INotifyPropertyChanged
    {
        IControllerModel Controller { get; }

        ObservableCollection<ICommanderModel> Commanders { get; }

        void SetData(ActorDO role);
    }
}
