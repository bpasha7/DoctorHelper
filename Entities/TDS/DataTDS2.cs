using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.TDS
{
    public class DataTDS2
    {
        public string[] Paragraphs { get; set; }

        public List<string> TableColumns { get; set; }
        public List<Criteria> Table { get; set; }

        public DataTDS2(List<Criteria> Values)
        {
            TableColumns = new List<string> { "", "Vps, см/сек", "RI", "Характер кровотока" };
            Table = Values;
        }
    }
}
