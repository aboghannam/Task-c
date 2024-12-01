﻿using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Application.Abstractions.Data;
using Common.Enums;
using Application.ClientData.Dtos;
using Common.Infrastructures;


namespace Application.ClientData.Commands
{
    public class CreateUserDataCommand: IRequest<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public UserType Type { get; set; }

        public class CreateUserDataValidator : AbstractValidator<CreateUserDataCommand>
        {
            public CreateUserDataValidator()
            {
                RuleFor(r => r.FirstName).NotEmpty().NotNull()
                    .WithMessage("FirstName is Required");
                RuleFor(r => r.LastName).NotEmpty().NotNull()
                    .WithMessage("LastName is Required");
                RuleFor(r => r.PhoneNumber).NotEmpty().NotNull()
                    .WithMessage("PhoneNumber is Required")
                    .Length(11)
                    .WithMessage("PhoneNumber musat be 11 number");

            }
        }

        public class Handler : IRequestHandler<CreateUserDataCommand, Result>
        {

            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(CreateUserDataCommand request, CancellationToken cancellationToken)
            {
                var userData = request.Adapt<UserData>();
                await _context.UserDatas.AddAsync(userData);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result(true, userData.Adapt<UserDataDto>(), "done");
            }
        }


    }
}
