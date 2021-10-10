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
            MySqlCommand cmd = new MySqlCommand("Insert", conn);

            cmd.Parameters.AddWithValue("@Name", patient.Name);
            cmd.Parameters.AddWithValue("@Birth", patient.Birth);
            cmd.Parameters.AddWithValue("@Cpf", patient.Cpf);
            foreach (Address A in patient.PAdresses)
            {
                AddressDAO.InsertAdress(A);
            }
            foreach (Email E in patient.EmailAddresses)
            {
                cmd.Parameters.AddWithValue("@email", E);
            }
            conn.Close();
        }
    }
}
