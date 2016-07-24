using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public abstract class Document : Entity, IDocument
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }


        public  void AssignFrom(IDocument doc)
        {

            if (doc == null)
            {
                return;
            }


            CreatedBy  = doc.CreatedBy;
            CreatedOn  = doc.CreatedOn;
            ModifiedBy = doc.ModifiedBy;
            ModifiedOn = doc.ModifiedOn;
        } 
             
    }
}
