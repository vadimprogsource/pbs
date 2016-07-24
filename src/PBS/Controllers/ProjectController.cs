using Api;
using Api.Sys;
using Microsoft.AspNetCore.Mvc;
using PBS.Models;
using Sys.Filtration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBS.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {

        private readonly IProjectServer projectServer;


        public ProjectController(IProjectServer ps)
        {
            projectServer = ps;
        }


        [HttpGet("{id}")]
        public IProject GetProject(Guid id)
        {
            return projectServer.GetProject(id);
        }



        [HttpGet]
        public IPageResult<IProject> GetProjects()
        {
            return GetProjects(new Filter());
        }  

        [HttpPost]
        public IPageResult<IProject> GetProjects([FromBody]Filter filter)
        {
            return projectServer.GetProjects(filter);
        }

        [HttpPut("create")]
        public IProject CreateProject([FromBody]CreateProjectModel model)
        {
            return projectServer.CreateProject(model.ProjectCode);
        }

        [HttpPut()]
        public IProject SaveProject([FromBody]UpdateProjectModel model)
        {
            return projectServer.UpdateProject(model);
        }


        [HttpDelete("{id}")]
        public bool RemoveProject(Guid id)
        {
            return projectServer.RemoveProject(id); 
        }
    }
}
