using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServices.Client;
using MicroServices.Models.Payments;
using NUnit.Framework;
using RestSharp;

namespace MicroServices.IntegrationTests
{
    [TestFixture]
    public class PaymentsIntegrationTests
    {
        [Category("Payments")]
        [Test]
        public async Task GetBankAccountReturnsListOfBankAccounts()
        {
            var bankAccounts = await PaymentsHelpers.GetBankAccountsForId(1).ConfigureAwait(false);

            Assert.AreEqual(1, bankAccounts.Count, "Incorrect number of bank accounts returned");

            var bankAccount = bankAccounts.First();

            Assert.AreEqual("12345678", bankAccount.AccountNumber);
            Assert.AreEqual(1, bankAccount.Id);
            Assert.AreEqual("123", bankAccount.RoutingNumber);

        }

    }

    public class PaymentsHelpers
    {
        private static readonly RestClient _client = ContractClientProvider.Client().Payments();

        public static async Task<List<BankAccount>> GetBankAccountsForId(int id)
        {
            var request = new RestRequest(String.Format("bank-accounts?id={0}", id), Method.GET);
            var response = await _client.ExecuteGetTaskAsync<List<BankAccount>>(request).ConfigureAwait(false);
            return response.Data;
        }
    }
}
