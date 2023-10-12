using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_4._5
{
   public class Books : Items
    {
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public List<string> Authors { get; set; }
        public string SelectedAuthors { get; set; }

    }
}
