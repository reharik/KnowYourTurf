using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Web.Controllers;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Services
{
    public class SandCalculator:ICalculatorHandler
    {
        public CalculatorViewModel GetViewModel()
        {
            return new SandCalcViewModel
            {
                CalculatorDisplayName = WebLocalizationKeys.SAND.ToString(),
                CalculatorName = WebLocalizationKeys.SAND.ToString(),
                CalculateUrl = UrlContext.GetUrlForAction<CalculatorController>(x => x.Calculate(null)),
//                SaveJSSuccssCallback = "kyt.calculator.controller.sandSuccess"
            };
        }

        public Continuation Calculate(SuperInputCalcViewModel input)
        {
            var continuation = new Continuation();
            var model = new SandCalcViewModel();

            double sand = (1.0/3.0)*3.14*(Math.Pow(input.Diameter/2,2))*input.Height/27.0;
            model.TotalSand = Convert.ToDouble(Math.Round(Convert.ToDecimal(sand), 2));
            continuation.Target = model;
            return continuation;
        }
    }
}