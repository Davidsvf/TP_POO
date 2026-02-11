// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 30/01/2026
// Description: lasse de ordenação de alojamentos por localização
// -------------------------------------------------

using System;
using System.Collections.Generic;
using Exceptions;

namespace BO
{
    /// <summary>
    /// Implementa a interface <see cref="IComparer{Alojamento}"/> para permitir a ordenação 
    /// de uma lista de alojamentos com base na sua localização geográfica.
    /// </summary>
    public class OrdenarAlojamentosPorLoc: IComparer<Alojamento>
    {

        /// <summary>
        /// Compara dois objetos <see cref="Alojamento"/> pela sua propriedade Localizacao.
        /// </summary>
        /// <param name="x">O primeiro alojamento a comparar.</param>
        /// <param name="y">O segundo alojamento a comparar.</param>
        /// <returns>
        /// Um valor que indica a posição relativa dos objetos na ordenação:
        /// Menor que zero: x é anterior a y.
        /// Zero: x é igual a y (ou um deles é nulo).
        /// Maior que zero: x é posterior a y.
        /// </returns>
        public int Compare(Alojamento x, Alojamento y)
        {
            if(x == null || y == null)
                return 0;

            return x.Localizacao.CompareTo(y.Localizacao);
        }
    }
}
