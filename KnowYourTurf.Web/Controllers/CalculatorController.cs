using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class CalculatorController : KYTController
    {
        private readonly IRepository _repository;

        public CalculatorController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Calculator(ViewModel input)
        {
            var calculator = _repository.Find<Calculator>(input.EntityId);
            var calculatorHandler = ObjectFactory.Container.GetInstance<ICalculatorHandler>(calculator.Name + "Calculator");
            CalculatorViewModel model = calculatorHandler.GetViewModel();
            model.EntityId = input.EntityId;
            return PartialView(calculator.Name+"Calculator", model);
        }

        public ActionResult Calculate(SuperInputCalcViewModel input)
        {
            var calculator = input.EntityId > 0 ? _repository.Find<Calculator>(input.EntityId) : new Calculator();
            var calculatorHandler = ObjectFactory.Container.GetInstance<ICalculatorHandler>(calculator.Name + "Calculator");
            var continuation = calculatorHandler.Calculate(input);
            if(!continuation.Success)
            {
                Notification notification = new Notification(continuation);
                return Json(notification, JsonRequestBehavior.AllowGet);
            }

            return Json(continuation.Target,JsonRequestBehavior.AllowGet);
        }
    }
}