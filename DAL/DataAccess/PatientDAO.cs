using HospitalCentral.DAL.Models;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class PatientDAO : IDAO
    {
        public static void InsertPatient(Patient patient)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            MySqlCommand cmdP = new MySqlCommand("INSERT INTO Patients(name, birth, cpf) VALUES (@name, @birth, @cpf)", conn);

            cmdP.Parameters.AddWithValue("@name", patient.Name);
            cmdP.Parameters.AddWithValue("@birth", patient.Birth);
            cmdP.Parameters.AddWithValue("@cpf", patient.Cpf);
            
            foreach (Address A in patient.PAdresses)
            {
                AddressDAO.InsertAdress(A);
            }
            foreach (Email E in patient.EmailAddresses)
            {
                EmailDAO.InsertEmail(E);
            }
            cmdP.ExecuteNonQuery();
            conn.Close();
        }
    }
}
