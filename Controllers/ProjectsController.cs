using DMAWS_T2203E_LaVietAnh.Context;
using DMAWS_T2203E_LaVietAnh.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2203E_LaVietAnh.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProjectsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projects = _context.Projects.ToArray();
            return Ok(projects);
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Created($"/get-by-id?id={project.ProjectId}", project);
        }

        [HttpPut]
        public IActionResult Update(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var projectDelete = _context.Projects.Find(id);
            if (projectDelete == null)
                return NotFound();
            _context.Projects.Remove(projectDelete);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("SearchProjects/{projectName}")]
        public IActionResult SearchProject(string projectName)
        {
            var projects = _context.Projects
                .Where(p => p.ProjectName.Contains(projectName))
                .ToList();

            return Ok(projects);
        }

        [HttpGet("InProgressProjects")]
        public IActionResult InProgressProjects()
        {
            var currentTime = DateTime.Now;
            var inProgressProjects = _context.Projects
                .Where(p => p.ProjectEndDate == null || p.ProjectEndDate > currentTime)
                .ToList();

            return Ok(inProgressProjects);
        }

        [HttpGet("FinishedProjects")]
        public IActionResult FinishedProjects()
        {
            var currentTime = DateTime.Now;
            var finishedProjects = _context.Projects
                .Where(p => p.ProjectEndDate != null && p.ProjectEndDate <= currentTime)
                .ToList();

            return Ok(finishedProjects);
        }

        [HttpGet]
        [Route("DetailsProject")]
        public IActionResult Get(int id)
        {
            var projects = _context.Projects.Where(e => e.ProjectId == id).Include(e => e.ProjectEmployees);
            if (projects == null)
                return NotFound();
            return Ok(projects);
        }

    }

}
