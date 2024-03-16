using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validate
{
    public class DomainValidation : Exception
    {
        public DomainValidation(string error) : base(error)
        {
            
        }

        public static void Validate(bool HasError, string Error)
        {
            if (HasError)
            {
                throw new DomainValidation(Error);
            }
        }

    }
}
