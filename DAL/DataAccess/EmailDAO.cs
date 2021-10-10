using HospitalCentral.DAL.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class EmailDAO
    {
        public static void InsertEmail(Email E)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            MySqlCommand cmd = new MySqlCommand("Insert", conn);

            cmd.Parameters.AddWithValue("@PatientCpf", E.PatientCpf);
            cmd.Parameters.AddWithValue("@EmailAddress", E.EmailAddress);
           
            conn.Close();
        }
    }
}
