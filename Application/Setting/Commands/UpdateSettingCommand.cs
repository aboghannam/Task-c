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
using Microsoft.EntityFrameworkCore;
using Application.Setting.Dtos;
using Microsoft.AspNetCore.Http;
using Common.Infrastructures;


namespace Application.ClientData.Commands
{
    public class UpdateSettingCommand : IRequest<Result>,IRegister
    {
        public string WorkDays { get; set; } //1,2,3,4,5,6
        public string WorkFrom { get; set; }
        public string WorkTo { get; set; }
        public double PeriodBetweenTimes { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateSettingCommand, Domain.Entities.Setting>()
              .Map(dest => dest.WorkDays, src => DateTime.ParseExact("2024-01-01 " + src.WorkFrom, "yyyy-MM-dd HH:mm", null))
              .Map(dest => dest.WorkTo, src => DateTime.ParseExact("2024-01-01 " + src.WorkTo, "yyyy-MM-dd HH:mm", null))
              ;

        }

        public class UpdateSettingValidator : AbstractValidator<UpdateSettingCommand>
        {
            public UpdateSettingValidator()
            {
                RuleFor(r => r.WorkDays).NotEmpty().NotNull()
                    .WithMessage("WorkDays is Required");
                RuleFor(r => r.WorkFrom).NotEmpty().NotNull()
                    .WithMessage("WorkFrom is Required");
                RuleFor(r => r.WorkTo).NotEmpty().NotNull()
                    .WithMessage("WorkTo is Required");
                RuleFor(r => r.PeriodBetweenTimes).NotEqual(0).NotNull()
                    .WithMessage("PhoneNumber is Required");

            }
        }

        public class Handler : IRequestHandler<UpdateSettingCommand, Result>
        {

            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
            {
                var setting = await _context.Settings.AsNoTracking().FirstOrDefaultAsync(); 
                var newSetting = request.Adapt<Domain.Entities.Setting>();

                if (setting == null)
                {
                    await _context.Settings.AddAsync(newSetting);
                }
                else
                {
                    newSetting.Id = setting.Id;
                    _context.Settings.Update(newSetting);
                }
                await _context.SaveChangesAsync(cancellationToken);

                return new Result(true, "done");
            }
        }


    }
}
