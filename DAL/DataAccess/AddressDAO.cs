using HospitalCentral.DAL.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class AddressDAO : IDAO
    {
        public static void InsertAdress (Address A)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            MySqlCommand cmd = new MySqlCommand("Insert", conn);

            cmd.Parameters.AddWithValue("@PatientCpf", A.PatientCpf);
            cmd.Parameters.AddWithValue("@Cep", A.Cep);
            cmd.Parameters.AddWithValue("@Cep", A.Cep);
            cmd.Parameters.AddWithValue("@State", A.State);
            cmd.Parameters.AddWithValue("@City", A.City);
            cmd.Parameters.AddWithValue("@Street", A.Street);
            cmd.Parameters.AddWithValue("@HouseNumber", A.HouseNumber);

            conn.Close();
        }
    }
}
