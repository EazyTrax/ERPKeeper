
using KeeperCore.ERPNode.Models.Profiles;
using KeeperCore.ERPNode.Models.Tasks;
using KeeperCore.ERPNode.Models.Tasks.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.DAL.Projects
{

    public class Projects : ERPNodeDalRepository
    {
        public Projects(Organization organization) : base(organization)
        {

        }

        public IQueryable<Project> Query() => erpNodeDBContext.Projects;
        public List<Project> ListAll() => erpNodeDBContext.Projects.ToList();
        public Project Find(Guid id) => erpNodeDBContext.Projects.Find(id);
        public IList<Project> GetListByStatus(ProjectStatus status) => this.Query().Where(t => t.Status == status).ToList();



        public Project Create(string name, string code)
        {
            var newProject = new Project()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Code = code,
                CreatedDate = DateTime.Now
            };

            erpNodeDBContext.Projects.Add(newProject);
            erpNodeDBContext.SaveChanges();
            return newProject;
        }

        public Project CreateNew(NewProjectModel newProjectModel)
        {
            var newProject = new Project()
            {
                Id = Guid.NewGuid(),
                Name = newProjectModel.Name,
                Code = "N/A",
                CreatedDate = DateTime.Now
            };

            erpNodeDBContext.Projects.Add(newProject);
            erpNodeDBContext.SaveChanges();
            return newProject;
        }

        public List<Models.Transactions.Commercial> GetCommercials(Guid id)
        {
            var project = this.Find(id);
            return project.Commercials.ToList();
        }


    }
}