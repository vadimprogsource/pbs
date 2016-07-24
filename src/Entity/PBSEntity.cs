using Api;
using Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class PBSEntity : Document , IPBSEntity
    {
        public int Index { get; set; }
        public int Level { get; set; }
        public string Code { get; set; }

        public int TechId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid RootId { get; set; }
        public Guid ParentId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }



        public PBSEntity Root { get; set; }

        public PBSEntity Parent { get; set; }
        
        public PBSEntity[] Children { get; set; }
       

        public User Owner { get; set; }



        public Technology Technology { get; set; }

        IPBSEntity IPBSEntity.Root
        {
            get
            {
                return Root;
            }
        }

        IPBSEntity IPBSEntity.Parent
        {
            get
            {
                return Parent;
            }
        }

        IPBSEntity[] IPBSEntity.Children
        {
            get
            {
                return Children;
            }
        }

        ITechnology IPBSEntity.Technology
        {
            get
            {
                return Technology;
            }
        }

        IPerson IPBSEntity.OwnedBy
        {
            get
            {
                return Owner;
            }
        }
    }
}
