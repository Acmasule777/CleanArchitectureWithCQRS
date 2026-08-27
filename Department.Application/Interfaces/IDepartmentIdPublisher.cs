using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Application.Interfaces
{
    public interface IDepartmentIdPublisher
    {
        public Task DepartmentIdPublishByName(GetDepartmentResponse responce);
    }
}
