namespace Manage.Offers.Exceptions
{
    using System;

    public class OfferNotFoundException : Exception
    {
        public OfferNotFoundException(int brokerId, string message) : base(message)
        {

        }
    }
}
