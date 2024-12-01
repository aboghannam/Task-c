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


namespace Application.ClientData.Queries
{
    public class GetUserQuery : IRequest<Result>
    {
        public Guid Id { get; set; }

        public class GetUserValidator : AbstractValidator<GetUserQuery>
        {
            public GetUserValidator()
            {
                RuleFor(r => r.Id).NotEmpty().NotNull()
                    .WithMessage("Id is Required");
            }
        }

        public class Handler : IRequestHandler<GetUserQuery, Result>
        {

            private readonly IApplicationDbContext _context;
            private readonly IValidator<GetUserQuery> _validator;


            public Handler(IApplicationDbContext context, IValidator<GetUserQuery> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<Result> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var userData = await _context.UserDatas.FindAsync(request.Id);
                if (userData == null)
                    return new Result(false, message: "not found");
                return new Result(true, userData.Adapt<UserDataDto>(), "done");
            }
        }


    }
}
