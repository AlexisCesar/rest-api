namespace Entidades.Models
{
    public class FuncionarioPJ : Funcionario
    {
        public FuncionarioPJ() : base() { }

        public override double CalcularSalario()
        {
            throw new NotImplementedException();
        }
    }
}
