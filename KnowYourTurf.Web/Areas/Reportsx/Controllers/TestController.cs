
using System;

namespace KnowYourTurf.Web.Areas.Reports.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CC.Core.CoreViewModelAndDTOs;
    using CC.Core.DomainTools;
    using CC.Core.Services;

    using Castle.Components.Validator;

    using KnowYourTurf.Core.Domain;
    using KnowYourTurf.Web.Controllers;
    using KnowYourTurf.Web.Config;

    public class TestController : KYTController
    {

        public TestController()
        {
        }
        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new ViewModel());
        }

      
    }
}
