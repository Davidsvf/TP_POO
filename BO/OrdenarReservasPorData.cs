// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 15/12/2026
// Description: Classe que implementa IComparer para ordenar objetos do tipo Reserva pela data de Check-in
// -------------------------------------------------

using System;
using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Classe utilitária que permite a ordenação de objetos <see cref="Reserva"/> 
    /// com base na data de início da estadia (Check-in).
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IComparer{BO.Reserva}" />
    public class OrdenarReservasPorData : IComparer<Reserva>
    {
        /// <summary>
        /// Compara duas reservas com base na data de Check-in (ordem cronológica).
        /// </summary>
        /// <param name="x">A primeira reserva a comparar.</param>
        /// <param name="y">A segunda reserva a comparar.</param>
        /// <returns>
        /// Um valor que indica a posição relativa na ordenação:
        /// Menor que zero: x ocorre antes de y.
        /// Zero: datas iguais ou objeto nulo.
        /// Maior que zero: x ocorre depois de y.
        /// </returns>
        public int Compare(Reserva x, Reserva y)
        {
            if(x == null || y == null)
                return 0;

            return x.DataCheckIn.CompareTo(y.DataCheckIn);
        }
    }
}
