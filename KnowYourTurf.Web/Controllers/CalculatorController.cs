using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class CalculatorController : KYTController
    {
        private readonly IRepository _repository;

        public CalculatorController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Calculator_Template(ViewModel input)
        {
            var calculator = _repository.Find<Calculator>(input.EntityId);
            var calculatorHandler = ObjectFactory.Container.GetInstance<ICalculatorHandler>(calculator.Name + "Calculator");
            return View(calculator.Name + "Calculator", calculatorHandler.EmptyViewModel());
        }

        public ActionResult Calculator(ViewModel input)
        {
            var calculator = _repository.Find<Calculator>(input.EntityId);
            var calculatorHandler = ObjectFactory.Container.GetInstance<ICalculatorHandler>(calculator.Name + "Calculator");
            CalculatorViewModel model = calculatorHandler.GetViewModel();
            model.EntityId = input.EntityId;
            return new CustomJsonResult(model);
        }

        public ActionResult Calculate(SuperInputCalcViewModel input)
        {
            var calculator = input.EntityId > 0 ? _repository.Find<Calculator>(input.EntityId) : new Calculator();
            var calculatorHandler = ObjectFactory.Container.GetInstance<ICalculatorHandler>(calculator.Name + "Calculator");
            var continuation = calculatorHandler.Calculate(input);
            if(!continuation.Success)
            {
                return new CustomJsonResult(continuation.ReturnNotification());
            }
            return new CustomJsonResult(continuation.Target);
        }
    }
}