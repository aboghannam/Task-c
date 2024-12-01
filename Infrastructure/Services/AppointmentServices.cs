using Application.Abstractions.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AppointmentServices(IApplicationDbContext _applicationDbContext) : IAppointmentServices
    {
        public async Task<bool> CheckTimeAvilable(DateTime dateTime)
        {
            var setting = await _applicationDbContext.Settings.FirstOrDefaultAsync() ?? new Domain.Entities.Setting();
            List<int> workingDays = setting.WorkDays?.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToList() ?? new List<int>();
            if (!workingDays.Contains((int)dateTime.DayOfWeek))
                return false;

            if (dateTime.TimeOfDay < setting.WorkFrom.TimeOfDay || dateTime.TimeOfDay > setting.WorkFrom.TimeOfDay)
                return false;

            return true;
        }
    }
}
