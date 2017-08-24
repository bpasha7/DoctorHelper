using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class DBEntitie
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
