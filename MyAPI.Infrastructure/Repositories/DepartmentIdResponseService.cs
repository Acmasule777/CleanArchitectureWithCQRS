using MyAPI.Application.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Repositories
{
    public class DepartmentIdResponseService :IDepartmentIdResponseService
    {

        private TaskCompletionSource<GetDepartmentResponse?> _response = new();

        public void SetResponse(GetDepartmentResponse response)
        {
            _response.SetResult(response);
        }


        public async Task<GetDepartmentResponse?> returnToHandler()
        {


            return await _response.Task;
        } 
    }
}
