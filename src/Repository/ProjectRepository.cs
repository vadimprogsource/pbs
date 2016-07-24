using Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api;
using Api.Sys;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProjectRepository :DocumentRepository<IProject,Project> ,  IProjectRepository
    {

        public ProjectRepository(IConfig cfg , ISecurityContext context) : base(cfg,context)
        {
        }


        public bool TryFindProject(string projectCode, out IProject project)
        {
            project = Set<Project>().AsNoTracking().FirstOrDefault(x => x.Code == projectCode);
            return project != null;
        }

        protected override Project OnCreate(IProject ifc)
        {
            return base.OnCreate  (ifc)
                       .AssignFrom(ifc);
       }

        protected override void OnUpdate(IProject ifc, Project obj)
        {
            base.OnUpdate(ifc, obj);
            obj.AssignFrom(ifc);
        }
    }
}
