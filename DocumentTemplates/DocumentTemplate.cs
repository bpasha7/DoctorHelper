using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentTemplates
{
    public abstract class DocumentTemplate : IDocumentTemplate, IDisposable
    {
        /// <summary>
        /// Название документа
        /// </summary>
        protected string Name { get; set; }
        /// <summary>
        /// Врач
        /// </summary>
        public string Owner { get; set; }
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
        public string Device { get; set; }
        /// <summary>
        /// Услвоия локации
        /// </summary>
        public string Location { get; set; }

        public virtual bool Print()
        {
            throw new NotImplementedException();
        }

        public virtual string Save()
        {
            throw new NotImplementedException();
        }

        public virtual bool SavePreview()
        {
            return false;
            //throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
