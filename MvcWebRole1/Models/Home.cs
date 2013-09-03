using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebRole1.Models
{
    public class Home
    {
        public string Version { get; set; }
        public string WebConfig { get; set; }
        public string WebRoleConfig { get; set; }

        public Home()
        {
            this.WebConfig = System.Configuration.ConfigurationManager.AppSettings["Setting"];
            this.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
            this.WebRoleConfig = Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("Environment");
        }

    }
}