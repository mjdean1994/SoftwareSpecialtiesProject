using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GATM.CloudServices;

namespace GATM.Web.Services
{
    class TransactionClientService : IAuthenticatedClientService
    {
        public TransactionClientService()
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

        /// <summary>
        /// Removes a given amount from the user's account
        /// </summary>
        /// <param name="amount">The amount to withdraw from the account</param>
        /// <returns>True if successful. Otherwise, false.</returns>
        public bool Withdraw(float amount)
        {
            TransactionServerService transactionServerService = new TransactionServerService();
            return transactionServerService.PostTransaction(TransactionType.Withdraw, amount, "NOT_AVAILABLE_IN_SIMULATION");
        }

        /// <summary>
        /// Adds a given amount from the user's account
        /// </summary>
        /// <param name="amount">The amount to deposit to the account</param>
        /// <returns>True if successful. Otherwise, false.</returns>
        public bool Deposit(float amount)
        {
            TransactionServerService transactionServerService = new TransactionServerService();
            return transactionServerService.PostTransaction(TransactionType.Deposit, amount, "NOT_AVAILABLE_IN_SIMULATION");
        }
    }
}
