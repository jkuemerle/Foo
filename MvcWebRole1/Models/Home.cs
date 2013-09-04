using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Home
    {
        public string Version { get; set; }
        public string WebConfig { get; set; }
        public string WebRoleConfig { get; set; }
        public string Build { get; set; }
      
        public Home()
        {
            this.WebConfig = System.Configuration.ConfigurationManager.AppSettings["Setting"];
            this.Build = System.Configuration.ConfigurationManager.AppSettings["Build"];
            this.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
#if AZURE
            this.WebRoleConfig = Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("Environment");
#endif
        }

    }
}