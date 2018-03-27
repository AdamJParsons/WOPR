using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.UI.WPF.Common.Helpers
{
    public class MappingHelper : IMappingHelper
    {
        public void AddPoint(Map map, double longitude, double latitude)
        {
            Pushpin pin = new Pushpin();
            pin.Location = new Location(latitude, longitude);
            map.Children.Add(pin);
            
        }
    }

    public interface IMappingHelper
    {
        void AddPoint(Map map, double longitude, double latitude);
    }
}
