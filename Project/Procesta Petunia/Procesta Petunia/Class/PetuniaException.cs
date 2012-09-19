using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Procesta_Petunia.Class
{
    class PetuniaException :Exception
    {
        public PetuniaException()
        {
            
        }
        public PetuniaException(string message)
            : base(message)
        {
            
        }
        public PetuniaException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected PetuniaException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
