using System;

namespace KnowYourTurf.Core.CoreViewModelAndDTOs
{
    public class OrderDetailsDto
    {
        public DateTime ExpirationDate { get; set; }
        public int SubscriptionMonths { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
    }
}