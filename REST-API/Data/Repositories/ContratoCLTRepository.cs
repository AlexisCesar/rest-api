using Entidades.Models;
using Microsoft.EntityFrameworkCore;

namespace Restful_API.Data
{
    public interface IContratoCLTRepository
    {
        Task<IEnumerable<ContratoCLT>> GetContratosAsync();
        Task<ContratoCLT> GetContratoByIdAsync(Guid contratoId);
        Task InsertContratoAsync(ContratoCLT contrato);
        Task DeleteContrato(Guid contratoId);
        void UpdateContrato(ContratoCLT contrato);
        Task SaveAsync();
    }

    public class ContratoCLTRepository : IContratoCLTRepository, IDisposable
    {
        private AppDbContext _context;
        public ContratoCLTRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteContrato(Guid contratoId)
        {
            ContratoCLT contrato = await GetContratoByIdAsync(contratoId);
            _context.ContratosCLT.Remove(contrato);
        }

        public async Task<ContratoCLT> GetContratoByIdAsync(Guid contratoId)
        {
            return await _context.ContratosCLT.Include(nameof(Contrato.Funcionario)).FirstOrDefaultAsync(x => x.Id == contratoId);
        }

        public async Task<IEnumerable<ContratoCLT>> GetContratosAsync()
        {
            return await _context.ContratosCLT.Include(nameof(Contrato.Funcionario)).AsNoTracking().ToListAsync();
        }

        public async Task InsertContratoAsync(ContratoCLT contrato)
        {
            await _context.ContratosCLT.AddAsync(contrato);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateContrato(ContratoCLT contrato)
        {
            _context.Entry(contrato).State = EntityState.Modified;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}