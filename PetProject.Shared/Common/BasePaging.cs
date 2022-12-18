using PetProject.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Shared.Common
{
    public abstract class BasePaging
    {
        public BasePaging(int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                throw new PetProjectException("The page index value must greate than or equal 0");
            }
            if (pageSize <= 0)
            {
                throw new PetProjectException("The page size value must greate than or equal 0");
            }
            if (pageIndex > pageSize)
            {
                throw new PetProjectException("The page size value must greate than or equal page index");
            }
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
