﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public Guid? UpdateBy { get; set; }
    }
}
