using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Repositories
{
    public class DepartmentRepoClient : IDepartmentServiceClient
    {
        private readonly HttpClient _httpClient;

        public DepartmentRepoClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DepartmentService");
        }

        public async Task<DepartmentDto?> GetDepartmentById(int departmentId)
        {
            var response = await _httpClient.GetAsync($"api/Department/{departmentId}");

            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<DepartmentDto>();
        }

        public async Task<List<DepartmentDto>> GetDepartmentsByIdsAsync(List<int> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Department/batch", ids);


            if (!response.IsSuccessStatusCode)
            {
                return new List<DepartmentDto>();
            }

            var result = await response.Content.ReadFromJsonAsync<List<DepartmentDto>>();

            return result ?? new List<DepartmentDto>();
        }

        public async Task<DepartmentDto?> GetDepartmentByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"api/Department/Byname/{Uri.EscapeDataString(name)}");

            if (!response.IsSuccessStatusCode)
                return null; // not found

            return await response.Content.ReadFromJsonAsync<DepartmentDto>();
        }


        public async Task<int> CreateDepartmentAsync(string name)
        {
            var payload = new DepartmentDto
            {
                DepartmentId = 0,           
                DepartmentName = name
            };

            var response = await _httpClient.PostAsJsonAsync($"api/Department/internal", payload);
            response.EnsureSuccessStatusCode();

            var newId = await response.Content.ReadFromJsonAsync<int>();
            return newId;
        }

    }
}
