using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCentral.DAL.Models
{
    public class Email
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required] //dependency
        public string PatientCpf { get; set; }

        public Email(string emailAddress, Patient patient)
        {
            Id = Guid.NewGuid();
            EmailAddress = emailAddress;
            PatientCpf = patient.Cpf;
        }
        public override bool Equals(object obj)
        {
            return obj is Email email &&
                   Id.Equals(email.Id) &&
                   EmailAddress == email.EmailAddress &&
                   PatientCpf == email.PatientCpf;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, EmailAddress, PatientCpf);
        }
    }
}
