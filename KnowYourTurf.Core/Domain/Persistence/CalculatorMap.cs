namespace KnowYourTurf.Core.Domain.Persistence
{
    public class CalculatorMap : DomainEntityMap<Calculator>
    {
        public CalculatorMap()
        {
            Map(x => x.Name);
        }
    }
}