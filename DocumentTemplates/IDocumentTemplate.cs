using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentTemplates
{
    public interface IDocumentTemplate
    {
        bool Print();
        string Save();
        bool SavePreview();
    }
}
