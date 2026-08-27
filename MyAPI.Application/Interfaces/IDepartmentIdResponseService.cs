using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Interfaces
{
    public interface IDepartmentIdResponseService
    {
        public void SetResponse(GetDepartmentResponse response);
        Task<GetDepartmentResponse?> returnToHandler();
    }
}
