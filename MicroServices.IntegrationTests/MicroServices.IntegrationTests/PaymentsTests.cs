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
    public class PaymentsTests
    {
        [Test]
        [Category("Payments")]
        public async Task GetBankAccountByAccountIdReturnsBankAccounts()
        {
            var bankAccounts = await PaymentsHelpers.GetBankAccountsForAccount(1).ConfigureAwait(false);
            Assert.AreEqual(1, bankAccounts.Count);
            var bankAccount = bankAccounts.First();

            Assert.AreEqual("12345678", bankAccount.AccountNumber);
            Assert.AreEqual("123", bankAccount.RoutingNumber);
            Assert.AreEqual(1, bankAccount.Id);
        }

    }

    public class PaymentsHelpers
    {
        private static readonly RestClient _client = ContractClientProvider.Client().Payments();

        public static async Task<List<BankAccount>> GetBankAccountsForAccount(int accountId)
        {
            var request = new RestRequest($"bank-accounts?id={accountId}", Method.GET);
            var response = await _client.ExecuteGetTaskAsync<List<BankAccount>>(request).ConfigureAwait(false);
            return response.Data;
        }
    }
}
