using Entidades.Models;
using Microsoft.EntityFrameworkCore;

namespace Restful_API.Data
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetFuncionariosAsync();
        Task<Funcionario> GetFuncionarioByIdAsync(Guid funcionarioId);
        Task InsertFuncionarioAsync(Funcionario funcionario);
        Task DeleteFuncionario(Guid funcionarioId);
        void UpdateFuncionario(Funcionario funcionario);
        Task SaveAsync();
    }

    public class FuncionarioRepository : IFuncionarioRepository, IDisposable
    {
        private AppDbContext _context;
        public FuncionarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteFuncionario(Guid funcionarioId)
        {
            Funcionario funcionario = await GetFuncionarioByIdAsync(funcionarioId);
            _context.Funcionarios.Remove(funcionario);
        }

        public async Task<Funcionario> GetFuncionarioByIdAsync(Guid funcionarioId)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == funcionarioId);
        }

        public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync()
        {
            return await _context.Funcionarios.AsNoTracking().ToListAsync();
        }

        public async Task InsertFuncionarioAsync(Funcionario funcionario)
        {
            await _context.Funcionarios.AddAsync(funcionario);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateFuncionario(Funcionario funcionario)
        {
            _context.Entry(funcionario).State = EntityState.Modified;
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