using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ExProperty : DBEntitie
    {
        public ExProperty()
        { }
        public ExProperty(DbDataReader Reader)
        {
            try
            {
                Id = Convert.ToInt32(Reader["Id"]);
                Name = Reader["Name"].ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
