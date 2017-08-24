using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentTemplates
{
    public abstract class DocumentTemplate
    {
        /// <summary>
        /// Название документа
        /// </summary>
        protected string Name { get; set; }
        /// <summary>
        /// Врач
        /// </summary>
        protected string Owner { get; set; }
        /// <summary>
        /// Пациент
        /// </summary>
        protected Client Client { get; set; }
        /// <summary>
        /// Дата обследования
        /// </summary>
        protected DateTime Date { get; set; }
        /// <summary>
        /// Аппарат
        /// </summary>
        protected string Device { get; set; }

    }
}
