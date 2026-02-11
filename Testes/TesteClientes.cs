// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 26/12/2025
// Description: Testes unitários às regras de negócio relacionadas com a inserção de clientes
// -------------------------------------------------

using System;
using BO;
using Regras;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes
{
    /// <summary>
    /// Classe de testes unitários para validar as regras de negócio associadas à gestão de clientes.
    /// </summary>
    [TestClass]
    public class TesteClientes
    {
        /// <summary>
        /// Testa a inserção de um cliente válido.
        /// </summary>
        [TestMethod]
        public void ValidaInserirCliente()
        {
            Cliente c = new Cliente("David", "123456789", "912345678", "david@gmail.com", new DateTime(2006, 5, 13));

            bool resultado = ServicoClientes.InserirCliente(c);

            Assert.IsTrue(resultado);
        }

        /// <summary>
        /// Testa a tentativa de inserção de um cliente com NIF duplicado.
        /// Deve lançar a exceção ClienteDuplicadoException.
        /// </summary>
        [TestMethod]
        public void ExceptionNifDuplicado()
        {
            Cliente c1 = new Cliente("David", "123456789", "912345678", "david@gmail.com", new DateTime(2006, 5, 13));

            Cliente c2 = new Cliente("Carolina", "123456789", "918888888", "carolina@gmail.com", new DateTime(2006, 5, 13));

            try
            {
                ServicoClientes.InserirCliente(c1);
            }
            catch (ClienteDuplicadoException e)
            {
                StringAssert.Contains(e.Message, "Já existe um cliente com este NIF");
            }
        }

        /// <summary>
        /// Testa a tentativa de inserção de um cliente nulo.
        /// Deve lançar a exceção ClienteInvalidoException.
        /// </summary>
        [TestMethod]
        public void ExceptionClienteNull()
        {
            Cliente c = null;

            try
            {
                ServicoClientes.InserirCliente(c);
            }
            catch (ClienteInvalidoException e)
            {
                StringAssert.Contains(e.Message, "O cliente não é válido");

            }

        }
    }
}
