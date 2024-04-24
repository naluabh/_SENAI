using System;
using System.Text.RegularExpressions;

namespace Atvd
{
    class Program
    {
        static void Main(string[] args)
        {
            float val_pag;
            Console.WriteLine("Informar Nome");
            string var_nome = Console.ReadLine();

            Console.WriteLine("Informar Endereço");
            string var_endereco = Console.ReadLine();

            Console.WriteLine("Pessoa Física (f) ou Jurídica (j) ?");
            string var_tipo = Console.ReadLine();

            if(var_tipo == "f")
            {
                // --- Pessoa Física ---
                Pessoa_Fisica pf = new Pessoa_Fisica(var_nome, var_endereco);

                string cpf;
                do
                {
                    Console.WriteLine("Informar CPF:");
                    cpf = LerCPF();
                    if(!ValidaCPF(cpf))
                    {
                        Console.WriteLine("CPF inválido! Digite novamente o CPF:");
                    }
                } while (!ValidaCPF(cpf));
                
                pf.cpf = cpf;

                Console.WriteLine("Informar RG:");
                pf.rg = Console.ReadLine();

                Console.WriteLine("Informar Valor de Compra:");
                val_pag = float.Parse(Console.ReadLine());
                pf.Pagar_Imposto(val_pag);

                ExibirInformacoes(pf);
            }

            if(var_tipo == "j")
            {
                // Pessoa Jurídica
                Pessoa_Juridica pj = new Pessoa_Juridica(var_nome, var_endereco);

                string cnpj;
                do
                {
                    Console.WriteLine("Informar CNPJ:");
                    cnpj = LerCNPJ();
                    if(!ValidaCNPJ(cnpj))
                    {
                        Console.WriteLine("CNPJ inválido! Digite novamente o CNPJ:");
                    }
                } while (!ValidaCNPJ(cnpj));
                
                pj.cnpj = cnpj;

                Console.WriteLine("Informar IE:");
                pj.ie = Console.ReadLine();

                Console.WriteLine("Informar Valor de Compra:");
                val_pag = float.Parse(Console.ReadLine());

                // Chamada do método Pagar_Imposto explicitamente
                pj.Pagar_Imposto(val_pag);

                ExibirInformacoes(pj);
            }
        }

        static void ExibirInformacoes(Clientes cliente)
        {
            Console.WriteLine("-------- Informações do Cliente --------");
            Console.WriteLine("Nome ..........: " + cliente.nome);
            Console.WriteLine("Endereço ......: " + cliente.endereco);
            Console.WriteLine("Valor de Compra: " + cliente.valor.ToString("C"));
            Console.WriteLine("Imposto .......: " + cliente.valor_imposto.ToString("C"));
            Console.WriteLine("Total a Pagar : " + cliente.total.ToString("C"));
        }

        static string LerCPF()
        {
            string cpf;
            do
            {
                cpf = Console.ReadLine();
                cpf = RemoverCaracteresEspeciais(cpf);
            } while (cpf.Length != 11);

            return cpf;
        }

        static string LerCNPJ()
        {
            string cnpj;
            do
            {
                cnpj = Console.ReadLine();
                cnpj = RemoverCaracteresEspeciais(cnpj);
            } while (cnpj.Length != 14);

            return cnpj;
        }

        static string RemoverCaracteresEspeciais(string input)
        {
            return Regex.Replace(input, "[^0-9]", ""); // Remove todos os caracteres não numéricos
        }

        static bool ValidaCPF(string cpf)
        {
            // Lógica de validação de CPF
            // Aqui você pode inserir a função ValidaCPF que você já tinha
            // ou utilizar a função que forneci anteriormente

            return true; // Apenas para exemplo, retornando sempre true neste caso
        }

        static bool ValidaCNPJ(string cnpj)
        {
            cnpj = cnpj.Replace("[^0-9]", ""); // Remove todos os caracteres não numéricos

            if (cnpj == "") return false;

            if (cnpj.Length != 14) return false;

            // Elimina CNPJs inválidos conhecidos
            if (cnpj == "00000000000000" || 
                cnpj == "11111111111111" || 
                cnpj == "22222222222222" || 
                cnpj == "33333333333333" || 
                cnpj == "44444444444444" || 
                cnpj == "55555555555555" || 
                cnpj == "66666666666666" || 
                cnpj == "77777777777777" || 
                cnpj == "88888888888888" || 
                cnpj == "99999999999999")
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];
            }

            int resto = (soma % 11);
            if (resto < 2)
            {
                if (int.Parse(cnpj[12].ToString()) != 0)
                    return false;
            }
            else
            {
                if (int.Parse(cnpj[12].ToString()) != 11 - resto)
                    return false;
            }

            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);
            if (resto < 2)
            {
                if (int.Parse(cnpj[13].ToString()) != 0)
                    return false;
            }
            else
            {
                if (int.Parse(cnpj[13].ToString()) != 11 - resto)
                    return false;
            }

            return true;
        }
    }
}
