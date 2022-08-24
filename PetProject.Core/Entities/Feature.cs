using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Core.Entities
{
    public class Feature : BaseEntity
    {
        public string? FeatureName { get; set; }
        public string? Description { get; set; }
        public int FeatureType { get; set; }
        public bool? IsFeature { get; set; }
        public byte Status { get; set; }
        public int Sequence { get; set; }
        public int? ParentId { get; set; }
    }
}
