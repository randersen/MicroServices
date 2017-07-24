using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using MicroServices.Accounts.Services;
using MicroServices.Models.Accounts;

namespace MicroServices.Accounts.Controllers
{
    [RoutePrefix("accounts")]
    public class AccountsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<Account> GetAccountById(int id)
        {
            return await AccountsService.GetAccount(id).ConfigureAwait(false);
        }
    }
}
