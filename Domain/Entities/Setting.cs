using Common.Enums;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Setting : BaseEntity
    {
        public string WorkDays { get; set; } //1,2,3,4,5,6
        public DateTime WorkFrom { get; set; }
        public DateTime WorkTo { get; set; }
        public double PeriodBetweenTimes { get; set; }
    }
}
