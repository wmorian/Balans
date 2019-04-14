using System;

namespace Balans.Models.Banks.DKB
{
    public class DkbEntity : Entity
    {
        public DateTime DateOfBooking { get; set; }

        public DateTime ValueDate { get; set; }

        public string BookingType { get; set; }

        public string Initiator { get; set; }

        public string Purpose { get; set; }

        public string AccountNumber { get; set; }

        public string BLZ { get; set; }

        public string CreditorId { get; set; }

        public string MandateReference { get; set; }

        public string ClientReference { get; set; }
    }
}
