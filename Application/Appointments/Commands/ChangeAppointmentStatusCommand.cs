using Domain.Entities;
using FluentValidation;
using MediatR;
using Mapster;
using Application.Abstractions.Data;
using Application.Appointments.Dtos;
using Application.Interfaces;
using Common.Infrastructures;
using Common.Enums;


namespace Application.Appointments.Commands
{
    public class ChangeAppointmentStatusCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Finished;

        public class ChangeAppointmentStatusValidator : AbstractValidator<ChangeAppointmentStatusCommand>
        {
            public ChangeAppointmentStatusValidator()
            {
                RuleFor(r => r.Id).NotEmpty().NotNull()
                    .WithMessage("Id is Required");

            }
        }

        public class Handler : IRequestHandler<ChangeAppointmentStatusCommand, Result>
        {

            private readonly IApplicationDbContext _context;
            private readonly IAppointmentServices _appointmentServices;

            public Handler(IApplicationDbContext context, IAppointmentServices appointmentServices)
            {
                _context = context;
                _appointmentServices = appointmentServices;
            }

            public async Task<Result> Handle(ChangeAppointmentStatusCommand request, CancellationToken cancellationToken)
            {
                var appointment = await _context.Appointments.FindAsync(request.Id);
                if (appointment.Status == AppointmentStatus.Finished)
                    return new Result(false, appointment.Adapt<AppointmentDto>(), "can't cancel finished orders");
                appointment.Status = request.Status;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result(true, appointment.Adapt<AppointmentDto>(), "done");
            }
        }

    }
}
