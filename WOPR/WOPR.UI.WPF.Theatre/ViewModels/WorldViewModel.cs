using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Infrastructure.Utilities.Logging;
using WOPR.UI.WPF.Common.Helpers;
using WOPR.UI.WPF.Common.ViewModels;

namespace WOPR.UI.WPF.Theatre.ViewModels
{
    public class WorldViewModel : ViewModelBase, IWorldViewModel
    {
        private readonly IMappingHelper m_MappingHelper;

        private readonly IWOPRLogger m_Logger;

        private const string API_KEY = "AvPUUOMfBSzSv5V0N2kwqCwA6cE9xyirmnsfKOIoiYl0I-Sgpugemm0cfdbrnzMs";

        private Map m_World;

        public CredentialsProvider MapCredentialsProvider
        {
            get
            {
                return new ApplicationIdCredentialsProvider(API_KEY); 
            }
        }

        public Map World
        {
            get
            {
                return m_World;
            }
            set
            {
                m_World = value;
                m_MappingHelper.AddPoint(value, -2.244644, 53.483959);
            }
        }

        public WorldViewModel(IMappingHelper mappingHelper, IWOPRLogger logger)
        {
            this.m_MappingHelper = mappingHelper;
            this.m_Logger = logger;
            logger.Log("test");
        }
    }

    public interface IWorldViewModel : IViewModelBase
    {
        Map World { get; set; }
    }
}
