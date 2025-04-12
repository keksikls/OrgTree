using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message, Exception inner): base(message, inner) 
        {

        }
    }

    public class PersistenceException : Exception
    {
        public PersistenceException(string message, Exception inner): base(message, inner) 
        {

        }
    }
}
