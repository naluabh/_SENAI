namespace Atvd
{
    class Pessoa_Juridica : Clientes
    {
        public string cnpj { get; set; }
        public string ie { get; set; }

        public Pessoa_Juridica(string nome, string endereco)
        {
            this.nome = nome;
            this.endereco = endereco;
        }

        public override void Pagar_Imposto(float v)
        {
            this.valor = v;
            this.valor_imposto = this.valor * 20 / 100;
            this.total = this.valor + this.valor_imposto;
        }
    }
}
