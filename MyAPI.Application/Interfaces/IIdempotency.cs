using MyAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Interfaces
{
    public interface IIdempotency
    {
        Task<IdempotencyEntity?> GetAsync(string Key, CancellationToken cancellationToken);
        Task CreateAsync(string key, string requestHash, string status, CancellationToken cancellationToken);

    }
    
}
