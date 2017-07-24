using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MicroServices.Payments.Models;
using MicroServices.Payments.Services;

namespace MicroServices.Payments.Controllers
{
    [RoutePrefix("bank-accounts")]
    public class BankAccountController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<ICollection<BankAccount>> GetBankAccountForAccount(int id)
        {
            return await BankAccountService.GetBankAccountsForAccount(id).ConfigureAwait(false);
        }
    }
}
