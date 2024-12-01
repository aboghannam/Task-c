using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Application.Abstractions.Data;
using Application.ClientData.Dtos;
using Application.Appointments.Dtos;
using Common.Infrastructures;
using Common.Enums;


namespace Application.Appointments.Queries
{
    public class GetAllAppointmentsQuery: IRequest<Result>
    {
        public Guid? AdminId { get; set; } = null;
        public Guid? PatientId { get; set; } = null;
        public DateTime? DateFrom { get; set; } = null;
        public DateTime? DateTo { get; set; } = null;
        public AppointmentStatus? Status { get; set; } = null;
        public class Handler : IRequestHandler<GetAllAppointmentsQuery, Result>
        {

            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Appointments.AsQueryable();
                if (request.AdminId != null)
                    query = query.Where(x => x.AdminId == request.AdminId);

                if (request.PatientId != null)
                    query = query.Where(x => x.PatientId == request.PatientId);

                if (request.DateFrom != null)
                    query = query.Where(x => x.ReservedTime >= request.DateFrom.Value);

                if (request.DateTo != null)
                    query = query.Where(x => x.ReservedTime <= request.DateTo.Value);

                if (request.Status.HasValue)
                    query = query.Where(x => x.Status == request.Status);

                var usersData = await query.ProjectToType<AppointmentDto>().ToListAsync();
                return new Result(true, usersData, "done");
            }
        }

    }
}
