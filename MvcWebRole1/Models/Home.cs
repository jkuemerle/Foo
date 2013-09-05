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
        public string DatabaseValue { get; set; }
        public Home()
        {
            this.WebConfig = System.Configuration.ConfigurationManager.AppSettings["Setting"];
            this.Build = System.Configuration.ConfigurationManager.AppSettings["Build"];
            this.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
            bool executeDB = false;
#if AZURE
            this.WebRoleConfig = Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("Environment");
            executeDB = Convert.ToBoolean(Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("executeDB"));
#else
            executeDB = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ExecuteDB"]);
#endif
            string conn;
#if AZURE
            conn = Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.GetConfigurationSettingValue("DBConn");
#else
            conn = System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ToString(); ;
#endif
            if (executeDB)
            {
                var cn = new System.Data.SqlClient.SqlConnection(conn);
                cn.Open();
                var cmd = new System.Data.SqlClient.SqlCommand("SELECT TOP 1 Col1 FROM Items", cn);
                this.DatabaseValue = cmd.ExecuteScalar().ToString();
                cn.Close();
            }
        }

    }
}