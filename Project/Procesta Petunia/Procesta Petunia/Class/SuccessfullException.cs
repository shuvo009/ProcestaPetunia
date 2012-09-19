using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Procesta_Petunia.Class
{
    class SuccessfullException : Exception
    {
        public SuccessfullException()
        {
            
        }
        public SuccessfullException(string message)
            : base(message)
        {
            
        }
        public SuccessfullException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected SuccessfullException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
