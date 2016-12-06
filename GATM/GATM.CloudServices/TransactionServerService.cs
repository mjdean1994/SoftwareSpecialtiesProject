using GATM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATM.CloudServices
{
    public enum TransactionType
    {
        Withdraw,
        Deposit
    }

    public class TransactionServerService : IAuthenticatedServerService
    {
        private Dictionary<string, DateTime> _authenticatedClients { get; set; }

        public TransactionServerService()
        {
            _authenticatedClients = new Dictionary<string, DateTime>();   
        }
        
        /// <summary>
        /// Attempts to validate the client service based on a submitted authentication string
        /// </summary>
        /// <param name="clientAddress">The IP address of the client device</param>
        /// <param name="authenticationString">The authentication string provided by the client device, generated from the AuthenticationService</param>
        /// <returns></returns>
        public bool Authenticate(string clientAddress, string authenticationString)
        {
            AuthenticationService authenticationService = new AuthenticationService();
            
            if(authenticationService.ValidateAuthenticationString(authenticationString))
            {
                _authenticatedClients.Add(clientAddress, DateTime.Now);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the client from the list of authenticated client devices
        /// </summary>
        /// <param name="clientAddress">The IP address of the client device</param>
        public void DetachClient(string clientAddress)
        {
            _authenticatedClients.Remove(clientAddress);
        }

        /// <summary>
        /// Attempts to process a monetary transaction as requested by the client
        /// </summary>
        /// <param name="type">The type of transaction (withdraw, deposit)</param>
        /// <param name="amount">The amount of money to adjust the account by</param>
        /// <returns>True if the transaction is successful. Otherwise false.</returns>
        public bool PostTransaction(TransactionType type, float amount, string clientAddress)
        {
            DateTime authenticationDateTime;
            _authenticatedClients.TryGetValue(clientAddress, out authenticationDateTime);
            if (authenticationDateTime == null)
            {
                return false;
            }

            if (type == TransactionType.Deposit)
            {
                return AccountRepository.AdjustAccountBalance(amount);
            }
            else if(type == TransactionType.Withdraw)
            {
                // Multiply by -1 because we're removing money from the account
                return AccountRepository.AdjustAccountBalance(amount * -1);
            }

            // If we get here, something went wrong on the client side
            return false;
            
        }
    }
}
