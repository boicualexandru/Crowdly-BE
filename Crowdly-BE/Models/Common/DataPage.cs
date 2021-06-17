using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.Common
{
    public class DataPage<T>
    {
        public T[] Data { get; set; }
        public bool HasMore { get; set; }
    }
}
