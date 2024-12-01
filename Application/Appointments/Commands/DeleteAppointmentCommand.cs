using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Common.Interfaces;
using Common.Infrastructures;
using Application.Appointments.Dtos;


namespace Application.ClientData.Commands
{
    public class DeleteAppointmentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public class DeleteAppointmentValidator : AbstractValidator<DeleteAppointmentCommand>
        {
            public DeleteAppointmentValidator()
            {
                RuleFor(r => r.Id).NotEmpty().NotNull()
                    .WithMessage("Id is Required");

            }
        }

        public class Handler(IApplicationDbContext _context, IDateTimeProvider _dateTimeProvider) : IRequestHandler<DeleteAppointmentCommand, Result>
        {

            public async Task<Result> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
            {
                var appointment = await _context.Appointments.FindAsync(request.Id);
                if (appointment == null)
                    return new Result(false, message: "check your id");

                appointment.IsDeleted = true;
                appointment.DeletedTime = _dateTimeProvider.UtcNow;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result(true, appointment.Adapt<AppointmentDto>(), "done");
            }
        }


    }
}
