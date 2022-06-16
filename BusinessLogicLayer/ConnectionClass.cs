using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BusinessLogicLayer
{
    public class ConnectionClass
    {
        public SqlConnection Con { get; set; }
        public SqlCommand Cmd { get; set; }
        public SqlParameter Param { get; set; }

        public bool ConnectionCheck(ref SqlConnection Con)
        {
            var ConnString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            try
            {
                Con = new SqlConnection(ConnString);
                Con.Open();
                if (Con.State == ConnectionState.Open)
                    return true;
                else
                    throw new Exception("Unable to connect database");
            }
            catch(Exception ex)
            {
                return false;
            }
        }



    }
}
