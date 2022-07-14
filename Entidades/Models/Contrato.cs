using Entidades.Models.Exceptions;

namespace Entidades.Models
{
    public abstract class Contrato
    {
        public Contrato(DateTime inicio, DateTime? termino, decimal salarioBruto, string cargo, Guid funcionarioId)
        {
            this.Id = Guid.NewGuid();
            this.Inicio = inicio;
            SetTermino(termino);
            SetSalarioBruto(salarioBruto);
            SetCargo(cargo);
            this.FuncionarioId = funcionarioId;

            checarPropriedades();
        }

        protected Contrato() { }

        public Guid Id { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime? Termino { get; private set; }
        public decimal SalarioLiquido { get; private set; }
        public decimal SalarioBruto { get; private set; }
        public string? Cargo { get; private set; }
        public Guid FuncionarioId { get; private set; }

        public void SetFuncionarioId(Guid id)
        {
            this.FuncionarioId = id;
        }

        public void SetSalarioBruto(decimal salarioBruto)
        {
            var salarioLiquido = CalcularSalarioLiquido(salarioBruto);

            validarSalario(salarioBruto, salarioLiquido);

            this.SalarioBruto = salarioBruto;
            this.SalarioLiquido = salarioLiquido;
        }

        public void SetCargo(string cargo)
        {
            this.Cargo = cargo;
        }

        public void SetTermino(DateTime? termino)
        {
            validarInicioTermino(this.Inicio, termino);
            this.Termino = termino;
        }

        public decimal CalcularSalarioLiquido(decimal salarioBruto)
        {
            return salarioBruto - CalcularDescontosSalario(salarioBruto) + CalcularBeneficiosSalario(salarioBruto);
        }

        public abstract decimal CalcularDescontosSalario(decimal salarioBruto);

        public abstract decimal CalcularBeneficiosSalario(decimal salarioBruto);

        private void validarInicioTermino(DateTime inicio, DateTime? termino)
        {
            if (termino != null)
                if (termino.GetValueOrDefault().Date < inicio.Date) throw new DomainValidationException("Data de término não pode ser menor do que início.");
        }

        private void validarSalario(decimal salarioBruto, decimal salarioLiquido)
        {
            if (salarioBruto < 0 || salarioLiquido < 0) throw new DomainValidationException("Salário não pode ser negativo.");
        }

        private void checarPropriedades()
        {
            validarInicioTermino(this.Inicio, this.Termino);
            validarSalario(this.SalarioBruto, this.SalarioLiquido);
        }
    }
}
