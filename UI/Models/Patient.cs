using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalCentral.Models
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
        [Required] 
        public Email EmailAddress { get; set; }
        [Required] //dependency
        public List<Address> PAdresses { get; set; } = new List<Address>();
        
        public Patient(string name, DateTime birth, string cpf, Address pAdress, Email emailaddress)
        {
            Id = Guid.NewGuid();
            Name = name;
            Birth = birth;
            Cpf = cpf;
            PAdresses.Add(pAdress);
            EmailAddress = emailaddress;
        }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   Id.Equals(patient.Id) &&
                   Name == patient.Name &&
                   Birth == patient.Birth &&
                   Cpf == patient.Cpf &&
                   EqualityComparer<Email>.Default.Equals(EmailAddress, patient.EmailAddress) &&
                   EqualityComparer<List<Address>>.Default.Equals(PAdresses, patient.PAdresses);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Birth, Cpf, EmailAddress, PAdresses);
        }
    }
}
