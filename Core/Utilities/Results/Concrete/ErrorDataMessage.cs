using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataMessage<T> : DataResult<T>
    {
        public ErrorDataMessage(T data) : base(data, false)
        {
        }

        public ErrorDataMessage(T data, string message) : base(data, false, message)
        {
        }

        public ErrorDataMessage(string message) : base(default, false, message) 
        {
            
        }
        public ErrorDataMessage() : base(default, false)
        {
            
        }
    }
}
