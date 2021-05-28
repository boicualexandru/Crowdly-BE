using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Vendors.Models
{
    public class DataPage<T>
    {
        public T[] Data { get; set; }
        public bool HasMore { get; set; }
    }
}
