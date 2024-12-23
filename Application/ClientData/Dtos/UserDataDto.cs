﻿using Application.Appointments.Dtos;
using Common.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientData.Dtos
{
    public class UserDataDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }

        public UserType Type { get; set; }

        public ICollection<AppointmentDto> AdminAppointments { get; set; } = new HashSet<AppointmentDto>();
        public ICollection<AppointmentDto> PatientAppointments { get; set; } = new HashSet<AppointmentDto>();

    }
}
