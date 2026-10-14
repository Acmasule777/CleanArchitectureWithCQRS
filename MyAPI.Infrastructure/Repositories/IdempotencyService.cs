using Microsoft.EntityFrameworkCore;
using MyAPI.Application.Interfaces;
using MyAPI.Core.Entities;
using MyAPI.Infrastructure.Persistancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Repositories
{
    public class IdempotencyService : IIdempotency
    {

        private readonly ApplicationDbContext _context;
        public IdempotencyService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IdempotencyEntity?> GetAsync(string Key, CancellationToken cancellationToken)
        {
            var entity = await _context.IdempotencyEntities
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(x => x.Key == Key);

            return entity;
        }
        public async Task CreateAsync(string key, string requestHash, string status, CancellationToken cancellationToken)
        {
            try
            {
                var newEntity = new IdempotencyEntity
                {
                    Key = key,
                    RequestHash = requestHash,
                    Status = status,
                    CompletedAtUtc = DateTime.Now
                };

                await _context.AddAsync(newEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

       
    }
}
