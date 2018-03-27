using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Domain.DataObjects
{
    public class LocationDO : BaseDO
    {
        public string Name { get; set; }

        public Position Position { get; set; }
    }

    public struct Position
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
