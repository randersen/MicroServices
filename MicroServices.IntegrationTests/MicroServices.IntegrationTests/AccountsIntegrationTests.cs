using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServices.Client;
using MicroServices.Models.Accounts;
using MicroServices.Models.Payments;
using NUnit.Framework;
using RestSharp;

namespace MicroServices.IntegrationTests
{
    [TestFixture]
    public class AccountsIntegrationTests
    {
        [Category("Payments")]
        [Category("Accounts")]
        [Test]
        public async Task GetAccountReturnsAccount()
        {
            var account = await AccountsHelpers.GetAccountById(1).ConfigureAwait(false);

            Assert.AreEqual(1, account.Id);
            Assert.AreEqual("Fred McFeddington", account.Name);

            Assert.AreEqual(1, account.BankAccounts.Count, "Incorrect number of bank accounts returned");

            var bankAccount = account.BankAccounts.First();

            Assert.AreEqual("12345678", bankAccount.AccountNumber);
            Assert.AreEqual(1, bankAccount.Id);
            Assert.AreEqual("123", bankAccount.RoutingNumber);

            Assert.AreEqual("123", bankAccount.RoutingNumber);

        }

    }

    public class AccountsHelpers
    {
        private static readonly RestClient _client = ContractClientProvider.Client().Accounts();

        public static async Task<Account> GetAccountById(int id)
        {
            var request = new RestRequest(string.Format("accounts?id={0}", id), Method.GET);
            var response = await _client.ExecuteGetTaskAsync<Account>(request).ConfigureAwait(false);
            return response.Data;
        }
    }
}
