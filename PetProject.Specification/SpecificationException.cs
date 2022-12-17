using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Specification
{
    public class SpecificationException: Exception
    {
        public SpecificationException(string message): base(message)
        {        }
    }
}
