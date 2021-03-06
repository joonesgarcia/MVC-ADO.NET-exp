using DAL.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class PatientDAO : IDAO
    {
        public static void Insert(Patient patient)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            MySqlCommand cmdP = new MySqlCommand("INSERT INTO patients(id,name, birth, cpf, email) VALUES (@id,@name, @birth, @cpf, @email)", conn);

            patient.Id = Guid.NewGuid();

            cmdP.Parameters.AddWithValue("@id", patient.Id);
            cmdP.Parameters.AddWithValue("@name", patient.Name);
            cmdP.Parameters.AddWithValue("@birth", patient.Birth);
            cmdP.Parameters.AddWithValue("@cpf", patient.Cpf);
            cmdP.Parameters.AddWithValue("@email", patient.Email);
                     
            cmdP.ExecuteNonQuery();
            conn.Close();

            foreach (Address address in patient.Adresses)
            {
                address.Id = Guid.NewGuid();
                address.PatientId = patient.Id;
                AddressDAO.InsertAdress(address);
            }
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
            conn.Close();
            return patients;
        }
        public static Patient FindById(Guid? id)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            string sqlQuery = "SELECT id, name, birth, cpf, email FROM patients WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            Patient tempPatient = new Patient();

            using (var header = cmd.ExecuteReader()) { 
                while (header.Read()) { 
                        tempPatient.Id = Guid.Parse(header["id"].ToString());
                        tempPatient.Name = header["name"].ToString();
                        tempPatient.Birth = DateTime.Parse(header["birth"].ToString());
                        tempPatient.Cpf = header["cpf"].ToString();
                        tempPatient.Email = header["email"].ToString();
                }
            }
            conn.Close();


            List<Address> addresses = AddressDAO.FindByPatientId(id.Value);

            if (addresses.Any())
            {
                tempPatient.Adresses = addresses;
            }
            else
            {
                tempPatient.Adresses.Add(new Address());
            }

            return tempPatient;
        }
        public static void Update(Guid? id, Patient p)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            string sqlQuery = "UPDATE patients SET name=@name, birth=@birth, cpf=@cpf, email=@email WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@birth", p.Birth);
            cmd.Parameters.AddWithValue("@cpf", p.Cpf);
            cmd.Parameters.AddWithValue("@email", p.Email);

            cmd.ExecuteNonQuery();
            conn.Close();


            foreach (Address address in p.Adresses.Where(a => a.Id == Guid.Empty))
            {
                address.Id = Guid.NewGuid();
                address.PatientId = p.Id;
                AddressDAO.InsertAdress(address);
            }

            foreach (Address address in p.Adresses.Where(a => a.Id != Guid.Empty))
            {
                AddressDAO.Update(address.Id,address);
            }

            var savedAddresses = AddressDAO.FindByPatientId(p.Id);

            foreach (var savedAddress in savedAddresses)
            {
                if (!p.Adresses.Any(a => a.Id == savedAddress.Id))
                {
                    AddressDAO.Delete(savedAddress);
                }
            }

        }

        public static void Delete(Guid id)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            string sqlQuery = "DELETE FROM patients WHERE id = @id;"; // ###################
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
