using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<UserData> UserDatas { set; get; }
        DbSet<Appointment> Appointments { set; get; }
        DbSet<Setting> Settings { set; get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}