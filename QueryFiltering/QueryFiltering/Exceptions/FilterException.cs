using System;

namespace QueryFiltering.Exceptions
{
    internal class FilterException : Exception
    {
        public FilterException(string message = null) : base(message)
        {
            
        }
    }
}
