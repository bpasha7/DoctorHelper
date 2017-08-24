using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Patient : DBEntitie
    {
        public DateTime BDate { get; set; }
        public string Gender { get; set; }
        public string Lname { get; set; }
        public string Info { get { return $"{FullName} {BDate.ToShortDateString()}"; } }
        public string FullName { get { return $"{Lname} {Name}"; } }
        public Patient()
        {

        }
        public Patient(DbDataReader Reader)
        {
            try
            {
                Id = Convert.ToInt32(Reader["Id"]);
                Name = Reader["Name"].ToString();
                BDate = Convert.ToDateTime(Reader["BDate"]);
                Gender = Reader["Gender"].ToString();
                Lname = Reader["Lname"].ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
