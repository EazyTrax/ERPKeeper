using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ERPKeeperCore.Web.Areas.API.Project
{
    public class SalesController : API_Project_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Sales
                .Where(m => m.ProjectId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
