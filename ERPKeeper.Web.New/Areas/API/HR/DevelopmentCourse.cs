using System;
using System.Linq;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.HR
{
    public class DevelopmentCoursesController : API_Employees_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.DevelopmentCourses.ToList();
            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Employees.DevelopmentCourse();
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            Organization.ErpCOREDBContext.DevelopmentCourses.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.DevelopmentCourses.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.DevelopmentCourses.First(a => a.Id == key);
            Organization.ErpCOREDBContext.DevelopmentCourses.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
