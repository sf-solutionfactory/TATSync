using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace SyncTAT
{
    public class AppSettings
    {
        SqlConnection con;
        Configuration config;
        public AppSettings()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        public string getConnectionString(string key)
        {
            return config.ConnectionStrings.ConnectionStrings[key].ConnectionString;
        }
        public void saveConfig(string key, string value)
        {
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            config.Save(ConfigurationSaveMode.Modified);
        }
        public bool openConn(string stricon)
        {
            con = new SqlConnection(stricon);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Close();
                con.Open();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
