using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TreeNode : DBEntitie
    {
        public int pId { get; set; }
        public int epId { get; set; }

        public TreeNode()
        {

        }

        public TreeNode(DbDataReader Reader)
        {
            Id = Convert.ToInt32(Reader["Id"]);
            Name = Reader["Name"].ToString();
            pId = Convert.ToInt32(Reader["pId"]);
            epId = Convert.ToInt32(Reader["epId"]);
        }
    }
}
