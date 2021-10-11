using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Nascimento")]
        [Required]
        public DateTime Birth { get; set; }
        [Display(Name = "CPF")]
        [Required]
        public string Cpf { get; set; }
        [Display(Name = "E-mail")]
        [Required] 
        public string Email { get; set; }
        [Required] //dependency
        public List<Address> Adresses { get; set; } = new List<Address>();
        
        public Patient(string name, DateTime birth, string cpf, Address pAdress, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Birth = birth;
            Cpf = cpf;
            Adresses.Add(pAdress);
            Email = email;
        }

        public Patient()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   Id.Equals(patient.Id) &&
                   Name == patient.Name &&
                   Birth == patient.Birth &&
                   Cpf == patient.Cpf &&
                   Email == patient.Email &&
                   EqualityComparer<List<Address>>.Default.Equals(Adresses, patient.Adresses);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Birth, Cpf, Email, Adresses);
        }
    }
}
