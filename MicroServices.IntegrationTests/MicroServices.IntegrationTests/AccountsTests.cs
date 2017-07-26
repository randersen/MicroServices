using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServices.Client;
using MicroServices.Models.Accounts;
using NUnit.Framework;
using RestSharp;

namespace MicroServices.IntegrationTests
{
    [TestFixture]
    public class AccountsTests
    {
        [Category("Payments")]
        [Category("Accounts")]
        [Test]
        public async Task GetAccountByAccountIdReturnsAccount()
        {
            var account = await AccountsHelpers.GetAccountById(1).ConfigureAwait(false);

            Assert.AreEqual(1, account.Id);
            Assert.AreEqual("Fred McFeddington", account.Name);
            Assert.AreEqual(1, account.BankAccounts.Count);

            var bankAccount = account.BankAccounts.First();
            Assert.AreEqual("12345678", bankAccount.AccountNumber);
            Assert.AreEqual("123", bankAccount.RoutingNumber);
            Assert.AreEqual(1, bankAccount.Id);
        }
    }

    public class AccountsHelpers
    {

        private static readonly RestClient _client = ContractClientProvider.Client().Accounts();

        public static async Task<Account> GetAccountById(int accountId)
        {
            var request = new RestRequest($"accounts?id={accountId}", Method.GET);
            var response = await _client.ExecuteGetTaskAsync<Account>(request).ConfigureAwait(false);
            return response.Data;
        }
    }
}
