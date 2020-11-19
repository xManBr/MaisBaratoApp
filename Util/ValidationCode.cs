using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercoplano.Maisbarato.Server.RESTful.Util
{
    public class ValidationCode
    {

        public static bool IsValidNumber(string codigoBarra)
        {   //Verifica se o número digitado não está vazio ou é nulo e se está entre 2 e 13.
            if (string.IsNullOrEmpty(codigoBarra) || !(codigoBarra.Length >= 2 && codigoBarra.Length <= 13))
                return false;
            else
            {      //Verifica se o número digitado é realmente um número.
                foreach (char caracter in codigoBarra)   //Com o foreach passamos caracter por caracter para dentro de uma 
                    if (!(char.IsNumber(caracter)))  //variável "caracter" e verificamos se ela é um número.
                        return false;            //Caso ache algo diferente de número retorna falso.      
                return true;//Sem erros, retorna verdadeiro.
            }
        }

        public static bool BarCode(string codigoBarra)
        {
            if (IsValidNumber(codigoBarra))
            {
                //Declaração de variáveis
                int i, somaTotal, somaPar = 0, somaImpar = 0, multiplo, digito;
                for (i = 0; i < (codigoBarra.Length - 1); i++) //Laço para percorrer a String flexível ao seu tamanho.
                {
                    if ((i + 1) % 2 == 0) //Verificação da posíção do número, se é par ou ímpar.
                        somaPar += (((int)(codigoBarra[i]) - 48) * 3); //Caso Par, multiplica-se por 3.
                    else somaImpar += (((int)(codigoBarra[i]) - 48) * 1); //Caso Impar, multiplica-se por 1.
                }
                somaTotal = somaPar + somaImpar; // Soma de todos resultados.
                if (somaTotal % 10 != 0 && somaTotal > 10) //Algoritmo para encontrar o múltiplo de 10 mais perto, igual ou maior.
                    multiplo = ((somaTotal / 10) + 1);
                else
                        if (somaTotal < 10)
                    multiplo = 1;
                else multiplo = somaTotal / 10;  //fim
                digito = (multiplo * 10) - somaTotal;//Diminui-se do múltiplo o valor da soma total.
                if (digito != ((int)(codigoBarra[codigoBarra.Length - 1]) - 48)) //Verifica-se o dígito é igual ao 13º número do barcode.
                    return false; //Caso não, retorna falso.
                return true;  //Caso igual, retorna verdadeiro.
            }
            else
            {
                return false;
            }
        }
    }
}