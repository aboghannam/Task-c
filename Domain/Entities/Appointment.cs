using Common.Enums;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Appointment : BaseEntity,IDeleteBase
    {
        public DateTime ReservedTime { get; set; }
        public Guid AdminId { get; set; }
        public Guid? PatientId { get; set; }
        public UserData Admin { get; set; }
        public UserData Patient { get; set; }
        public AppointmentStatus Status { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
