using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Admin
{
    public class Paging<T> where T: class
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalPages {get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        //public int PageCount { get; set; }

    }
}
