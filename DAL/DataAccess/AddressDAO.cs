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
            MySqlCommand cmdA = new MySqlCommand("INSERT INTO Addresses(cep, state, city, street, houseNumber, patientCpf) VALUES (@cep, @state, @city, @street, @houseNumber, @patientCpf)", conn);

            cmdA.Parameters.AddWithValue("@patientCpf", A.PatientCpf);
            cmdA.Parameters.AddWithValue("@cep", A.Cep);
            cmdA.Parameters.AddWithValue("@state", A.State);
            cmdA.Parameters.AddWithValue("@city", A.City);
            cmdA.Parameters.AddWithValue("@street", A.Street);
            cmdA.Parameters.AddWithValue("@houseNumber", A.HouseNumber);

            cmdA.ExecuteNonQuery();
            conn.Close();
        }
    }
}
