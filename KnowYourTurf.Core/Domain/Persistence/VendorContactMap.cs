
namespace KnowYourTurf.Core.Domain.Persistence
{
    public class VendorContactMap : DomainEntityMap<VendorContact>
    {
        public VendorContactMap()
        {
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Address1);
            Map(x => x.Address2);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.ZipCode);
            Map(x => x.Country);
            Map(x => x.Email);
            Map(x => x.Fax);
            Map(x => x.Status);
            Map(x => x.Phone);
            Map(x => x.Notes);
            References(x => x.Vendor);
        }

      
    }
}