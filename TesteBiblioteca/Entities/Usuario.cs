namespace TesteBiblioteca.Entities
{
    public class Usuario : Pessoa
    {
        public Endereco Endereco { get; set; }
        public Emprestimo Emprestimo { get; set; }
    }
}
