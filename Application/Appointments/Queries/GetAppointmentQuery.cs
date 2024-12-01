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
using Application.ClientData.Commands;
using Application.Abstractions.Data;
using Application.ClientData.Dtos;
using Common.Infrastructures;
using Application.Appointments.Dtos;


namespace Application.Appointments.Queries
{
    public class GetAppointmentQuery : IRequest<Result>
    {
        public Guid Id { get; set; }

        public class GetAppointmentValidator : AbstractValidator<GetAppointmentQuery>
        {
            public GetAppointmentValidator()
            {
                RuleFor(r => r.Id).NotEmpty().NotNull()
                    .WithMessage("Id is Required");
            }
        }

        public class Handler : IRequestHandler<GetAppointmentQuery, Result>
        {

            private readonly IApplicationDbContext _context;


            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
            {
                var appointment = await _context.Appointments.FindAsync(request.Id);
                if (appointment == null)
                    return new Result(false, message: "not found");
                return new Result(true, appointment.Adapt<AppointmentDto>(), "done");
            }
        }


    }
}
