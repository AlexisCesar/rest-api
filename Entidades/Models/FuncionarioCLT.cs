namespace Entidades.Models
{
    public class FuncionarioCLT : Funcionario
    {
        public FuncionarioCLT() : base() { }

        public override double CalcularSalario()
        {
            throw new NotImplementedException();
        }
    }
}
