using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.UI.WPF.Simulation.Models.Platforms
{
    public class PlatformBase
    {
    }

    public interface IPlatformModel
    {
        ObservableCollection<IAsset> Assets { get; }
    }

    public interface IAsset
    {

    }
}
