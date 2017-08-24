using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Examination : DBEntitie
    {
        public string Short { get; set; }
        public Examination()
        { }
        public Examination(DbDataReader Reader)
        {
            try
            {
                Id = Convert.ToInt32(Reader["Id"]);
                Name = Reader["Name"].ToString();
                Short = Reader["Short"].ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
