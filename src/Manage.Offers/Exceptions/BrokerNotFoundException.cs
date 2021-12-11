namespace Manage.Offers.Exceptions
{
    using System;
    public class BrokerNotFoundException : Exception
    {
        public BrokerNotFoundException(int brokerId, string message) : base(message)
        {

        }
    }
}
