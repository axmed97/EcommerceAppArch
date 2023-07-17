﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Abstract
{
    public interface IResultData<T> : IResult
    {
        public T Data { get; }
    }
}
