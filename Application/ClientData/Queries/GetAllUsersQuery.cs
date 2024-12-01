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
using Common;


namespace Application.ClientData.Queries
{
    public class GetAllUsersQuery: IRequest<Result>
    {
        public class Handler : IRequestHandler<GetAllUsersQuery, Result>
        {

            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var usersData = await _context.UserDatas.ToListAsync();
                return new Result(true, usersData, "done");
            }
        }


    }
}
