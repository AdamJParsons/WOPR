using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects;

namespace WOPR.UI.WPF.Simulation.Models
{
    public class LocationModel : ModelBase<LocationDO>, ILocationModel
    {
        public LocationModel(LocationDO location)
            : base(location)
        {
        }
    }

    public interface ILocationModel : IModel<LocationDO>
    {
    }
}
