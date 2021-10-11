using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCentral.Models
{
    public class Email
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required] //dependency
        public Guid PatientId { get; set; }

        public Email(string emailAddress, Patient patient)
        {
            Id = Guid.NewGuid();
            EmailAddress = emailAddress;
            PatientId = patient.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Email email &&
                   Id.Equals(email.Id) &&
                   EmailAddress == email.EmailAddress &&
                   PatientId.Equals(email.PatientId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, EmailAddress, PatientId);
        }
    }
}
