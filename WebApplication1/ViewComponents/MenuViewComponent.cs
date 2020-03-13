using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.ViewComponents
{
    /// <summary>
    /// FROM MICROSOFT.DOC:
    /// we made the class inherit from ViewComponent so that it becomes a ViewComponent class.
    /// we could also just decorate it with the attribute [ViewComponent]
    /// View components are intended anywhere you have reusable rendering logic that's too complex for a partial view, such as:
    /// A login panel that would be rendered on every page and show either the links to log out or log in, depending on the log in state of the user
    /// </summary>

    public class MenuViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("Administrator"))
                return View("Default");
                else
                return View("NoMenuLinks");
            }
            else
            {
                return View("NoMenuLinks");
            }
            //this will return the view Views/Shared/Components/Menu/Default.cshtml bcuz (read below)
            /* FROM MICROSOFT.DOCS
             The default view name for a view component is Default, which means your view file will typically be named Default.cshtml.
             You can specify a different view name when creating the view component result or when calling the View method.
             */

        }
    }
}
