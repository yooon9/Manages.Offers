namespace Manage.Offers.Exceptions
{
    using System;

    public class ForeignKeyException : Exception
    {
        public ForeignKeyException(string message) : base(message)
        {

        }
    }
}
