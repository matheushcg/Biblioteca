﻿namespace TesteBiblioteca.Entities
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string ISBN { get; set; }

        public Status Status { get; set; }
    }
}
