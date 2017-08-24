using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Criteria
    {
        public string Name { get; set; }
        public string Val0 { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Val3 { get; set; }
        public string Val4 { get; set; }
        public string Val5 { get; set; }
        public string Val6 { get; set; }
        public string Val7 { get; set; }
        public string Val8 { get; set; }
        public string Val9 { get; set; }
        public string Val10 { get; set; }
        public string Val11{ get; set; }
        public string Val12 { get; set; }

        private int MaxVals = 9;

        //public List<string> Values { get; set; }
        public Criteria(string CriteriaName)
        {
            Name = CriteriaName;
            //Values = new List<string>();
        }
        public List<string> GetVluesList()
        {
            var Values = new List<string>();
            Values.Add(Val0);
            Values.Add(Val1);
            Values.Add(Val2);
            Values.Add(Val3);
            Values.Add(Val4);
            Values.Add(Val5);
            Values.Add(Val6);
            Values.Add(Val7);
            Values.Add(Val8);
            Values.Add(Val9);
            Values.Add(Val10);
            Values.Add(Val11);
            Values.Add(Val12);
            return Values;
        }
    }
}
