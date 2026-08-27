using Azure.Core;
using Department.Application.Interfaces;
using Department.Infrastructure.Persistency;
using DepartmentCore.Core.DTOs;
using DepartmentCore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;


namespace Department.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartment
    {
        private readonly AppDepartmentDbContext _context;
        private readonly IDepartmentIdPublisher _publisher;
        public DepartmentRepository(AppDepartmentDbContext context, IDepartmentIdPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<int> AddAsync(DepartmentDto department)
        {
            await _context.Departments.AddAsync(new DepartmentEntity
            {
                DepartmentName = department.DepartmentName
            });
            await _context.SaveChangesAsync();

            var Id = _context.Departments
                    .Where(d => d.DepartmentName == department.DepartmentName)
                    .Select(d => d.DepartmentId)
                    .FirstOrDefault();
            return Id;
        }


        public async Task<DepartmentDto?> GetByNameAsync2(GetDepartmentRequest request)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentName.ToLower() == request.DepartmentName.ToLower());

            if (department is null)
                return null;

            var DepartmentResponse = new GetDepartmentResponse
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                CorrelationId = request.CorrelationId
            };

            await _publisher.DepartmentIdPublishByName(DepartmentResponse);

            return new DepartmentDto();


            /*return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };*/
        }





        public async Task<DepartmentDto?> GetByNameAsync(string name)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentName.ToLower() == name.ToLower());

            if (department is null)
                return null;

            //var DepartmentResponse = new GetDepartmentResponse
            //{
            //    DepartmentId = department.DepartmentId,
            //    DepartmentName = department.DepartmentName,
            //    CorrelationId = request.CorrelationId
            //};

            // await _publisher.DepartmentIdPublishByName(DepartmentResponse);

            //return new DepartmentDto();


            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }

        public async Task<List<DepartmentDto>> GetAllDepartment()
        {
            return await _context.Departments.Select(e => new DepartmentDto
            {
                DepartmentId = e.DepartmentId,
                DepartmentName = e.DepartmentName
            }).ToListAsync();
        }

        public async Task<DepartmentDto?> GetDepartmentById(int id)
        {
            return await _context.Departments.Select(e => new DepartmentDto
            {
                DepartmentId = e.DepartmentId,
                DepartmentName = e.DepartmentName
            }).FirstOrDefaultAsync(d => d.DepartmentId == id);

        }

        public async Task<List<DepartmentDto>> GetDepartmentByIds(List<int> Ids)
        {
            return await _context.Departments
           .Where(d => Ids.Contains(d.DepartmentId))
           .Select(d => new DepartmentDto { DepartmentId = d.DepartmentId, DepartmentName = d.DepartmentName})
           .ToListAsync();
        }

        public async Task<string> CreateDepartment(DepartmentDto department)
        {
            var DepartmentNameExists = await _context.Departments.AnyAsync(d => d.DepartmentName.ToLower() == department.DepartmentName.ToLower());

            if (DepartmentNameExists)
            {
                return "Department is already exists";
            }

            _context.Departments.Add(new DepartmentEntity
            {
                DepartmentName = department.DepartmentName
            });

            await _context.SaveChangesAsync();
            return "Department Successfully Created";
        }

        public async Task<string> UpdateDepartment(UpdateDepartmentDto department)
        {
            var Dmt = await _context.Departments.FindAsync(department.DepartmentId);

            Dmt.DepartmentName = string.IsNullOrWhiteSpace(Dmt.DepartmentName) ? Dmt.DepartmentName = Dmt.DepartmentName : Dmt.DepartmentName = department.DepartmentName;

            _context.Departments.Update(Dmt);
            await _context.SaveChangesAsync();
            return "Department Updated Successfully";
        }

        public async Task<string> DeleteDepartment(int id)
        {
            var Dmt = await _context.Departments.FindAsync(id);

            if (Dmt == null)
                return "Not Found";

            _context.Departments.Remove(Dmt);
            await _context.SaveChangesAsync();
            return "Department Deleted Successfully";
        }
    }
}
