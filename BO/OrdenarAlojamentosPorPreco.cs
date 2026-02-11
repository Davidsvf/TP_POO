// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 15/12/2025
// Description: Classe que implementa IComparer para ordenar objetos do tipo Alojamento pelo preço por noite
// -------------------------------------------------

using System;
using System.Collections.Generic;
using Exceptions;

namespace BO
{
    /// <summary>
    /// Classe utilitária que permite a ordenação de objetos <see cref="Alojamento"/> 
    /// com base no valor do preço por noite (ordem ascendente).
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IComparer{BO.Alojamento}" />
    public class OrdenarAlojamentosPorPreco:IComparer<Alojamento>
    {
        /// <summary>
        /// Compara dois alojamentos com base no respetivo preço por noite.
        /// </summary>
        /// <param name="x">O primeiro alojamento a comparar.</param>
        /// <param name="y">O segundo alojamento a comparar.</param>
        /// <returns>
        /// Um valor inteiro que indica a posição relativa: 
        /// Menor que zero (x é mais barato), Zero (preços iguais ou nulos) ou Maior que zero (x é mais caro).
        /// </returns>
        public int Compare(Alojamento x, Alojamento y)
        {
            if(x==null || y==null) 
                return 0;
            
            return x.PrecoPorNoite.CompareTo(y.PrecoPorNoite);
        }
    }
}
