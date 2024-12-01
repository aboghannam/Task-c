using Common.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Appointments.Dtos
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime ReservedTime { get; set; }
        public Guid AdminId { get; set; }
        public Guid? PatientId { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
