using GATM.CloudServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GATM.Web.Services
{
    public class AccountClientService : IAuthenticatedClientService
    {
        public AccountClientService()
        {
            _isAuthenticated = false;
        }

        private bool _isAuthenticated { get; set; }

        /// <summary>
        /// Attempts to authenticate the client using user-provided credentials
        /// </summary>
        /// <param name="cardGuid">The unique identifier of the inserted ATM card</param>
        /// <param name="pin">The four-digit PIN entered by the user</param>
        /// <returns>True if the client is authenticated, false if authentication fails</returns>
        public bool Authenticate(string cardGuid, string pin)
        {
            AuthenticationService authenticationService = new AuthenticationService();
            string authenticationString = authenticationService.GenerateAuthenticationString(cardGuid, pin);

            TransactionServerService transactionServerService = new TransactionServerService();
            _isAuthenticated = transactionServerService.Authenticate("NOT_AVAILABLE_IN_SIMULATION", authenticationString);
            return _isAuthenticated;
        }

        public void Detach()
        {
            TransactionServerService transactionServerService = new TransactionServerService();
            transactionServerService.DetachClient("NOT_AVAILABLE_IN_SIMULATION");
        }

        public float GetAccountBalance()
        {
            if(!_isAuthenticated)
            {
                return -1;
            }

            AccountServerService accountServerService = new AccountServerService();
            return accountServerService.GetAccountBalance("NOT_AVAILABLE_IN_SIMULATION");
        }
    }
}