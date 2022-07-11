using Entidades.Models.Exceptions;

namespace Entidades.Models
{
    public class Funcionario
    {
        public Funcionario(string nome, string email)
        {
            this.Id = Guid.NewGuid();
            this.Nome = nome;
            this.Email = email;

            checarPropriedades();
        }
        
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public void SetNome(string nome) {
            validarNome(nome);
            this.Nome = nome;
        }

        public void SetEmail(string email) {
            validarEmail(email);
            this.Email = email;
        }

        private void validarNome(string nome) {
            if(nome.Length < 5)
                throw new DomainValidationException("Nome precisa ter pelo menos 5 caracteres.");
        }

        private void validarEmail(string email) {
            if(email.Length < 5)
                throw new DomainValidationException("Email precisa ter pelo menos 5 caracteres.");
        }

        private void checarPropriedades() {
            validarNome(this.Nome);
            validarEmail(this.Email);
        }

    }
}
