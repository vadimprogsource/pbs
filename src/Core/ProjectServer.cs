using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Sys;
using Api.Repository;
using Entity;

namespace Core
{
    public class ProjectServer : IProjectServer
    {

        private readonly IProjectRepository projectRepository;



        public ProjectServer(IProjectRepository pr)
        {
            projectRepository = pr;
        }

        public IProject CreateProject(string projectCode)
        {

            IProject project;

            if (projectRepository.TryFindProject(projectCode, out project))
            {
                return project;
            }

            return projectRepository.Insert(new Project { Code = projectCode, Title = projectCode });
        }

        public IProject GetProject(Guid id)
        {
            return projectRepository.Select(id);
        }

        public IPageResult<IProject> GetProjects(IFilter filter)
        {
            return projectRepository.Select(filter);
        }

        public bool RemoveProject(Guid id)
        {
            return projectRepository.Delete(id);
        }

        public IProject UpdateProject(IProject project)
        {
            return projectRepository.Update(project);
        }
    }
}
