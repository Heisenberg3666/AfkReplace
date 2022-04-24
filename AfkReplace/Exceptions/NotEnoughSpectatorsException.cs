using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfkReplace.Exceptions
{
    public class NotEnoughSpectatorsException : Exception
    {
        public NotEnoughSpectatorsException(string msg) : base(msg)
        {
        }
    }
}
