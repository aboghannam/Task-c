using Domain.Entities;
using FluentValidation;
using MediatR;
using Mapster;
using Application.Abstractions.Data;
using Application.Appointments.Dtos;
using Application.Interfaces;
using Common.Infrastructures;


namespace Application.Appointments.Commands
{
    public class CreateAppointmentCommand : IRequest<Result>
    {
        public DateTime ReservedTime { get; set; }
        public Guid AdminId { get; set; }
        public Guid? PatientId { get; set; }

        public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
        {
            public CreateAppointmentValidator()
            {
                RuleFor(r => r.AdminId).NotEmpty().NotNull()
                    .WithMessage("AdminId is Required");
                RuleFor(r => r.PatientId).NotEmpty().NotNull()
                    .WithMessage("PatientId is Required");

            }
        }

        public class Handler : IRequestHandler<CreateAppointmentCommand, Result>
        {

            private readonly IApplicationDbContext _context;
            private readonly IAppointmentServices _appointmentServices;

            public Handler(IApplicationDbContext context, IAppointmentServices appointmentServices)
            {
                _context = context;
                _appointmentServices = appointmentServices;
            }

            public async Task<Result> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
            {
                if (!await _appointmentServices.CheckTimeAvilable(request.ReservedTime))
                    return new Result(false, "choose another time");

                var appointment = request.Adapt<Appointment>();
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result(true, appointment.Adapt<AppointmentDto>(), "done");
            }
        }


    }
}
