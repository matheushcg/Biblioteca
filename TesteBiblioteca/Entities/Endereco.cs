using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBiblioteca.Entities
{
    public class Endereco
    {
        public Guid Id { get; set; }

        public string Rua { get; set; }

        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Cep { get; set; }
    }
}
