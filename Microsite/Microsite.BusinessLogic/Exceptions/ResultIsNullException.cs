using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsite.BusinessLogic.Exceptions
{
    class ResultIsNullException:Exception
    {
        public ResultIsNullException()
        {

        }
        public ResultIsNullException(string message) : base(message) { }
        public ResultIsNullException(string message, Exception inner) : base(message, inner) { }
    }
}
