using HospitalCentral.Models;
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
            MySqlCommand cmdP = new MySqlCommand("INSERT INTO Patients(name, birth, cpf, emailaddress) VALUES (@name, @birth, @cpf, @emailaddress)", conn);

            cmdP.Parameters.AddWithValue("@name", patient.Name);
            cmdP.Parameters.AddWithValue("@birth", patient.Birth);
            cmdP.Parameters.AddWithValue("@cpf", patient.Cpf);
            cmdP.Parameters.AddWithValue("@emailaddress", patient.EmailAddress);

            foreach (Address A in patient.PAdresses)
            {
                AddressDAO.InsertAdress(A);
            }
            
            cmdP.ExecuteNonQuery();
            conn.Close();
        }
    }
}
