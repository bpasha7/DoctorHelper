using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TDS
{
    /// <summary>
    /// данные для Транскраниальное дуплексное сканирование
    /// </summary>
    public class DataTDS
    {
        #region 1st Table
        public List<string> Table1Columns { get; set; }
        public List<Criteria> Table1 { get; set; }
        #endregion
        #region 2nd Table
        public List<Criteria> Table2 { get; set; }
        #endregion
        #region 3rd Table
        public List<string> Table3Columns { get; set; }
        public List<Criteria> Table3 { get; set; }
        #endregion
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public DataTDS(List<Criteria> Values1, List<Criteria> Values2, List<Criteria> Values3)
        {
            //if (Values1.Count != 5)
            //    throw new Exception("Транскраниальное дуплексное сканирование недостаточно даных для формирования таблицы!");
            Table1Columns = new List<string> { "Критерии", "ОСА(d)", "ОСА(s)", "ВСА(d)", "ВСА(s)", "ПА(d)", "ПА(s)", "ПКА(d)", "ПКА(s)" };
            Table1 = Values1;
            Table3Columns = new List<string> { "Критерии", "СМА", "ПМА", "ЗМА", "ПА", "ОА", "в. Розенталя" };
            Table3 = Values3;
            Table2 = Values2;       
        }
    }
}
