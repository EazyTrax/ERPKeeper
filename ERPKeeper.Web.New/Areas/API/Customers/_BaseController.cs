using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Customers
{
    [Route("/API/{CompanyId}/Customers/{controller}/{action=Index}")]
    public class Base_CustomersController : APIBaseController
    {
 

    }
}
