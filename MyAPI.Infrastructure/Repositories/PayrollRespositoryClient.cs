using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Repositories
{
    public class PayrollRespositoryClient : IPayrollRepositoryClient
    {
        private readonly HttpClient _httpClient;
        public PayrollRespositoryClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("PayrollService");
        }

        public async Task<PayrollServiceDto?> GetPayrollById(int empId)
        {
            var response = await _httpClient.GetAsync($"api/Payroll/ByEmpId/{empId}");
            //if(response == null)
            //{
            //    return null;
            //}
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<PayrollServiceDto?>();
        }

        public async Task<List<PayrollServiceDto>> GetAllPayrollsById(List<int> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Payroll/payrollBatch", ids);
            if (!response.IsSuccessStatusCode)
            {
                return new List<PayrollServiceDto>();
            }

            var result = await response.Content.ReadFromJsonAsync<List<PayrollServiceDto>>();
            return  result ?? new List<PayrollServiceDto>();
        }
    }
}
