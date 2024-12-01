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
using Common;
using Application.ClientData.Dtos;
using Application.Setting.Dtos;


namespace Application.Setting.Queries
{
    public class GetSettingQuery : IRequest<Result>
    {

        public class GetSettingValidator : AbstractValidator<GetSettingQuery>
        {
            public GetSettingValidator()
            {
            }
        }

        public class Handler : IRequestHandler<GetSettingQuery, Result>
        {

            private readonly IApplicationDbContext _context;
            private readonly IValidator<GetSettingQuery> _validator;


            public Handler(IApplicationDbContext context, IValidator<GetSettingQuery> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<Result> Handle(GetSettingQuery request, CancellationToken cancellationToken)
            {
                SettingDto result = new SettingDto();
                var setting = await _context.Settings.FirstOrDefaultAsync();
                if (setting == null)
                    return new Result(true, result, message: "done");
                result.WorkFrom = setting.WorkFrom;
                result.WorkTo = setting.WorkTo;
                result.PeriodBetweenTimes = setting.PeriodBetweenTimes;
                result.WorkDays = setting.WorkDays.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => x).ToList() ?? new List<string>();

                return new Result(true, result, "done");
            }
        }


    }
}
