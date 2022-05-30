using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Core.Entities
{
    public class Country : BaseEntity
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
