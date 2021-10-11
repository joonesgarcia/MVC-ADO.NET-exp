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
            MySqlCommand cmdE = new MySqlCommand("INSERT INTO Patients(emailAddress, patientCpf) VALUES (@emailAddress, @patientCpf)", conn);

            cmdE.Parameters.AddWithValue("@patientCpf", E.PatientCpf);
            cmdE.Parameters.AddWithValue("@emailAddress", E.EmailAddress);

            cmdE.ExecuteNonQuery();
            conn.Close();
        }
    }
}
