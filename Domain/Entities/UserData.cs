using Common.Enums;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserData : BaseEntity,IDeleteBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }

        public UserType Type { get; set; }

        public ICollection<Appointment> AdminAppointments { get; set; } = new HashSet<Appointment>();
        public ICollection<Appointment> PatientAppointments { get; set; } = new HashSet<Appointment>();
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
