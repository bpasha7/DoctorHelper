using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Docs : DBEntitie
    {
        public DateTime Date { get; set; }
        public int exId { get; set; }
        public int Pid { get; set; }
    }
}
