


using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.My.Views
{
    public abstract class BasePage<TModel> : ERPKeeperCore.Web.Views.Enterprise_BasePage<TModel>
    {


     

        protected BasePage()
        {

        }
    }
}
