﻿
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Projects;
using ERPKeeperCore.Enterprise.Models.Projects.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Projects : ERPNodeDalRepository
    {
        public Projects(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Project> GetAll()
        {
            return erpNodeDBContext.Projects.ToList();
        }
        public List<Project> GetActive()
        {
            return erpNodeDBContext.Projects
                .Where(p => p.Status == ProjectStatus.Open)
                .ToList();
        }

        public int Count()
        {
            return erpNodeDBContext.Projects.Count();
        }

        public Project? Find(Guid Id) => erpNodeDBContext.Projects.Find(Id);

        public object Create(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public void UpdateBalance()
        {
            foreach (var project in erpNodeDBContext.Projects.ToList())
            {
                project.UpdateBalance();
            }

            erpNodeDBContext.SaveChanges();
        }
    }
}