using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCentral.DAL.Models
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        public string Cpf { get; set; }       
        [Required] //dependency
        public List<Address> PAdresses { get; set; } = new List<Address>();
        [Required] //dependency
        public List<Email> EmailAddresses { get; set; } = new List<Email>();

        public Patient(string name, DateTime birth, string cpf, Address pAdress, Email emailaddress)
        {
            Id = Guid.NewGuid();
            Name = name;
            Birth = birth;
            Cpf = cpf;
            PAdresses.Add(pAdress);
            EmailAddresses.Add(emailaddress);
        }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   Id.Equals(patient.Id) &&
                   Name == patient.Name &&
                   Birth == patient.Birth &&
                   Cpf == patient.Cpf &&
                   EqualityComparer<List<Address>>.Default.Equals(PAdresses, patient.PAdresses) &&
                   EqualityComparer<List<Email>>.Default.Equals(EmailAddresses, patient.EmailAddresses);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Birth, Cpf, PAdresses, EmailAddresses);
        }
    }
}
