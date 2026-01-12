using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.HR.EmployeePaymentPeriod
{
    [Route("/API/HR/EmployeePaymentPeriods/{Id:Guid}/{controller}/{action=Index}")]
    public class _API_HR_EmployeePaymentPeriod_BaseController : API_BaseController
    {
        public Guid EmployeePaymentPeriodId => Guid.Parse(RouteData.Values["Id"].ToString());


    }
}
