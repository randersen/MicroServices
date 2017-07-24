using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroServices.Payments.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }

    }
}