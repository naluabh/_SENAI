namespace Atvd
{
    class Pessoa_Fisica : Clientes
    {
        public string cpf { get; set; }
        public string rg { get; set; }

        public Pessoa_Fisica(string nome, string endereco)
        {
            this.nome = nome;
            this.endereco = endereco;
            Pagar_Imposto(0);
        }
    }
}
