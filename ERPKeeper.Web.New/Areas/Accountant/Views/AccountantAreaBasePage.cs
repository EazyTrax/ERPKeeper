using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Accountant.Views
{
    public abstract class AccountantAreaBasePage<TModel> : ERPKeeperCore.Web.Views.Enterprise_BasePage<TModel>
    {

        protected AccountantAreaBasePage()
        {

        }
    }
}
