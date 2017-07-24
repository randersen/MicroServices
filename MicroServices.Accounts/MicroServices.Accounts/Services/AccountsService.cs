using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MicroServices.Models.Accounts;
using MicroServices.Models.Payments;
using RestSharp;

namespace MicroServices.Accounts.Services
{
    public class AccountsService
    {

        public static async Task<Account> GetAccount(int accountId)
        {
            var account = new Account()
            {
                Id = 1,
                Name = "Fred McFeddington",
                BankAccounts = await GetBankAccountsForAccount(accountId).ConfigureAwait(false)            
            };

            return account;
        }

        public static async Task<List<BankAccount>> GetBankAccountsForAccount(int id)
        {
            var client = new RestClient("http://localhost:50660/");
            var request = new RestRequest($"bank-accounts?id={id}");
            var accounts = await client.ExecuteGetTaskAsync<List<BankAccount>>(request).ConfigureAwait(false);
            return accounts.Data;
        }

    }
}