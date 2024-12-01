﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IDeleteBase
    {
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}