// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 08/11/2025
// Description: Interface para objetos que podem ser reservados
// -------------------------------------------------

using System;

namespace BO
{
    /// <summary>
    /// Define métodos base essenciais relacionados com reservas do alojamento.
    /// </summary>
    public interface IReservavel
    {
        /// <summary>
        /// Calcula o preço total da estadia com base no número de noites.
        /// </summary>
        /// <param name="noites">Número de noites da estadia.</param>
        /// <returns></returns>
        decimal CalcularPrecoTotal(int noites);

        /// <summary>
        /// Atualiza o estado de disponibilidade do alojamento.
        /// </summary>
        /// <param name="estado">if set to <c>true</c> [estado].</param>
        void AlterarDisponibilidade(bool estado);

    }
}
