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
using Common;


namespace Application.ClientData.Commands
{
    public class DeleteUserDataCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public class DeleteUserDataValidator : AbstractValidator<DeleteUserDataCommand>
        {
            public DeleteUserDataValidator()
            {
                RuleFor(r => r.Id).NotEmpty().NotNull()
                    .WithMessage("Id is Required");

            }
        }

        public class Handler : IRequestHandler<DeleteUserDataCommand, Result>
        {

            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(DeleteUserDataCommand request, CancellationToken cancellationToken)
            {
                var userData = await _context.UserDatas.FindAsync(request.Id);
                if (userData == null)
                    return new Result(false, message: "check your id");
                _context.UserDatas.Remove(userData);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result(true, userData, "done");
            }
        }


    }
}
