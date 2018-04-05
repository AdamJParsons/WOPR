using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;
using WOPR.UI.WPF.Simulation.Models.Platforms;

namespace WOPR.UI.WPF.Simulation.Models.MissionElements.StrategicAssets
{
    public class StrategicAssetBaseModel<TAsset> : ModelBase<TAsset>, IStrategicAsset<TAsset>
        where TAsset : StrategicAssetBaseDO
    {
        private ILocationModel m_Target;

        public ILocationModel Target
        {
            get
            {
                return m_Target;
            }
            set
            {
                m_Target = value;
                OnPropertyChanged("Target");
            }
        }

        public StrategicAssetBaseModel(TAsset asset)
            : base(asset)
        {          
        }

        public void Launch()
        {
            throw new NotImplementedException();
        }
    }

    public interface IStrategicAsset<TAsset> : IModel<TAsset>, IStrategicAsset
        where TAsset : StrategicAssetBaseDO
    {
    }

    public interface IStrategicAsset : IAsset
    {
        ILocationModel Target { get; set; }

        void Launch();
    }
}
