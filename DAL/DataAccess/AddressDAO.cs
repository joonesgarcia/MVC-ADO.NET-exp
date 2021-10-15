using DAL.Models;
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
            MySqlCommand cmdA = new MySqlCommand("INSERT INTO addresses(id,cep, state, city, street, houseNumber, patientid) VALUES (@id,@cep, @state, @city, @street, @houseNumber, @patientid)", conn);

            cmdA.Parameters.AddWithValue("@id", A.Id);
            cmdA.Parameters.AddWithValue("@cep", A.Cep);
            cmdA.Parameters.AddWithValue("@state", A.State);
            cmdA.Parameters.AddWithValue("@city", A.City);
            cmdA.Parameters.AddWithValue("@street", A.Street);
            cmdA.Parameters.AddWithValue("@houseNumber", A.HouseNumber);
            cmdA.Parameters.AddWithValue("@patientid", A.PatientId);

            cmdA.ExecuteNonQuery();
            conn.Close();
        }
        public static List<Address> FindAll()
        {
            MySqlConnection conn = IDAO.DAOConnect();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM addresses", conn);
            List<Address> addresses = new List<Address>();

            using(var header = cmd.ExecuteReader())
            {               
                while (header.Read())
                {
                    addresses.Add(MapperFromDataReader(header));
                }
            }
            return addresses;
        }
        public static Address FindById(Guid? id)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            //string sqlQuery = "SELECT name, cep, state, city, street, houseNumber FROM addresses JOIN patients WHERE patient_id=@id AND patients.id=patient_id;";
            string sqlQuery = "SELECT id, cep, state, city, street, houseNumber,patientid FROM addresses WHERE id=@id;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            Address tempAddress = new Address();

            using (var header = cmd.ExecuteReader())
            {
                while (header.Read())
                {
                    tempAddress = MapperFromDataReader(header);
                }
            }
            conn.Close();
            return tempAddress;
        }
        public static List<Address> FindByPatientId(Guid patientId)
        {
            MySqlConnection conn = IDAO.DAOConnect();            
            string sqlQuery = "SELECT id, cep, state, city, street, houseNumber,patientid FROM addresses WHERE patientid=@patientid;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@patientid", patientId);
            List<Address> tempAddresses = new List<Address>();

            using (var header = cmd.ExecuteReader())
            {
                while (header.Read())
                {
                    tempAddresses.Add(MapperFromDataReader(header));
                }
            }
            conn.Close();
            return tempAddresses;
        }
        public static void Update(Guid? id, Address p)
        {
            MySqlConnection conn = IDAO.DAOConnect(); 
            string sqlQuery = "UPDATE addresses SET cep=@cep, state=@state, city=@city, street=@street, houseNumber= @houseNumber WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@cep", p.Cep);
            cmd.Parameters.AddWithValue("@state", p.State);
            cmd.Parameters.AddWithValue("@city", p.City);
            cmd.Parameters.AddWithValue("@street", p.Street);
            cmd.Parameters.AddWithValue("@houseNumber", p.HouseNumber);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void Delete(Address a)
        {
            MySqlConnection conn = IDAO.DAOConnect();
            string sqlQuery = "DELETE FROM addresses WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@id", a.Id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private static Address MapperFromDataReader(MySqlDataReader header)
        {
            Address tempAddress = new Address();

            tempAddress.Id = Guid.Parse(header["id"].ToString()); // 
            tempAddress.Cep = header["cep"].ToString();
            tempAddress.State = header["state"].ToString();
            tempAddress.City = header["city"].ToString();
            tempAddress.Street = header["street"].ToString();
            tempAddress.HouseNumber = Int32.Parse(header["houseNumber"].ToString());
            tempAddress.PatientId = Guid.Parse(header["patientid"].ToString());

            return tempAddress;
        }
    }
}
