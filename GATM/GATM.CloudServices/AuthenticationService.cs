using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATM.CloudServices
{
    public class AuthenticationService
    {
        public AuthenticationService()
        {
            // In reality, this would come from application configuration
            // App config is not available in this simulation
            _generationSeed = 0;
        }

        private int _generationSeed { get; set; }

        /// <summary>
        /// Returns an authentication string based on the credentials provided and a class-specific string
        /// </summary>
        /// <param name="cardGuid">The unique identification string for the inserted ATM card</param>
        /// <param name="pin">A four-digit pin, entered by the user</param>
        /// <returns>An authentication string based on the given credentials</returns>
        public string GenerateAuthenticationString(string cardGuid, string pin)
        {
            return String.Format("{0}:{1}-{2}", cardGuid, pin, _generationSeed);
        }

        /// <summary>
        /// Validates an authentication string given from a client device
        /// </summary>
        /// <param name="authenticationString">Client-provided authentication string</param>
        /// <returns>The result of the validation as a boolean value. True is returned if the string is valid</returns>
        public bool ValidateAuthenticationString(string authenticationString)
        {
            //Unable to authenticate in simulation. Always return true.
            return true;
        }
    }
}
