using DAL.Models;
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
            MySqlCommand cmdP = new MySqlCommand("INSERT INTO patients(name, birth, cpf, email) VALUES (@name, @birth, @cpf, @email)", conn);

            cmdP.Parameters.AddWithValue("@name", patient.Name);
            cmdP.Parameters.AddWithValue("@birth", patient.Birth);
            cmdP.Parameters.AddWithValue("@cpf", patient.Cpf);
            cmdP.Parameters.AddWithValue("@email", patient.Email);

            foreach (Address A in patient.Adresses)
            {
                AddressDAO.InsertAdress(A);
            }
            
            cmdP.ExecuteNonQuery();
            conn.Close();
        }
        public static List<Patient> FindAll()
        {
            MySqlConnection conn = IDAO.DAOConnect();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM patients", conn);
            List<Patient> patients = new List<Patient>();

            using (var header = cmd.ExecuteReader())
            {              
                while (header.Read())
                {
                    Patient tempPatient = new Patient();

                    tempPatient.Id = Guid.Parse(header["id"].ToString());
                    tempPatient.Name = header["name"].ToString();
                    tempPatient.Birth = DateTime.Parse(header["birth"].ToString());
                    tempPatient.Cpf = header["cpf"].ToString();
                    tempPatient.Email = header["email"].ToString();

                    patients.Add(tempPatient);
                } 
            }
            return patients;
        }
    }
}
