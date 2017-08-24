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
        public string Value{ get; set; }

        public Template()
        { }
        public Template(DbDataReader Reader)
        {
         //   var props = this.GetType().GetProperties( BindingFlags.Public | BindingFlags.Instance);

            try
            {
        //        props[0].SetValue(this, "Sdfdsf", null);

                // props[1].PropertyType.FullName


                //Template obj = new Template();
                //foreach (var prop in props)
                //{
                //    props.SetValue(this, "Sdfdsf", null);
                //}
                //if (null != prop && prop.CanWrite)
                //{
                //    prop.SetValue(obj, "Value", null);
                //}
                //var myPropertyInfo = Type.GetType(this.GetType().Name).GetProperties();// (BindingFlags.Public | BindingFlags.Instance);
                //Console.WriteLine("Properties of System.Type are:");
                //for (int i = 0; i < myPropertyInfo.Length; i++)
                //{
                //    Console.WriteLine(myPropertyInfo[i].ToString());
                //}
                Id = Convert.ToInt32(Reader["Id"]);
                Name = Reader["Name"].ToString();
                Value = Reader["Value"].ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
