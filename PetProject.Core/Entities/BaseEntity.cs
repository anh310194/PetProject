using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Core.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}
