using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements.StrategicAssets;

namespace WOPR.UI.WPF.Simulation.Models.MissionElements.StrategicAssets
{
    public class IntercontinentalBallisticMissileModel : StrategicAssetBaseModel<IntercontinentalBallisticMissileDO>
    {
        public IntercontinentalBallisticMissileModel(IntercontinentalBallisticMissileDO missile)
            : base(missile)
        {
        }
    }
}
