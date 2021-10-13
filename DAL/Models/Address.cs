using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "CEP")]
        [Required]
        [MaxLength(9)]
        public string Cep { get; set; }
        [Display(Name = "Estado")]
        [Required]
        public string State { get; set; }
        [Display(Name = "Cidade")]
        [Required]
        public string City { get; set; }
        [Display(Name = "Rua")]
        [Required]
        public string Street { get; set; }
        [Display(Name = "Número")]
        [Required]
        public int HouseNumber { get; set; }
        [Required] //dependency
        public Guid PatientId { get; set; }

        public Address(string cep, string state, string city, string street, int houseNumber, Patient patient)
        {
            Id = Guid.NewGuid();
            Cep = cep;
            State = state;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PatientId = patient.Id;
        }

        public Address()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Id.Equals(address.Id) &&
                   Cep == address.Cep &&
                   State == address.State &&
                   City == address.City &&
                   Street == address.Street &&
                   HouseNumber == address.HouseNumber &&
                   PatientId.Equals(address.PatientId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Cep, State, City, Street, HouseNumber, PatientId);
        }
    }
}
