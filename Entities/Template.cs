using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace Entities
{
    public class Template : DBEntitie, IDBEntitie, IBaseEntity
    {
        //public string Value{ get; set; }

        public Template()
        { }
        public Template(DbDataReader Reader)
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
