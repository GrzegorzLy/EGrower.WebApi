using EGrower.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EGrower.Core.Repositories
{
    public interface IAtachmentRepository
    {
        Task AddAsync(Atachment atachment);
        Task<Atachment> GetAsync(int id);
        Task<IEnumerable<Atachment>> BrowseByEmailMessageIdAsync(int emailMessageId);
        Task UpdateAsync(Atachment atachment);
        Task DeleteAsync(Atachment atachment);
    }
}
