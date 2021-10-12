using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class IDAO
    {
        public static MySqlConnection DAOConnect()
        {
            MySqlConnection conn = new MySqlConnection(Environment.GetEnvironmentVariable("STRING_CONEXAO_BD"));
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return conn;
        }
    }
}
