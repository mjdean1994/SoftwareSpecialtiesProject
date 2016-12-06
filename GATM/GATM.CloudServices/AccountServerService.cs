using GATM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATM.CloudServices
{
    public class AccountServerService : IAuthenticatedServerService
    {
        private Dictionary<string, DateTime> _authenticatedClients { get; set; }

        public AccountServerService()
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

            if (authenticationService.ValidateAuthenticationString(authenticationString))
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
        
        public float GetAccountBalance(string clientAddress)
        {
            DateTime authenticationDateTime;
            _authenticatedClients.TryGetValue(clientAddress, out authenticationDateTime);
            if(authenticationDateTime == null)
            {
                return -1;
            }

            return AccountRepository.GetAccountBalance();
        }
    }
}
