using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message)
        {
        }
        public InternalServerException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public InternalServerException() : base("An internal server error occurred.")
        {
        }
    }
}
