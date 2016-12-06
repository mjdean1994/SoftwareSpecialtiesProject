using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATM.CloudServices
{
    interface IAuthenticatedServerService
    {
        bool Authenticate(string clientAddress, string authenticationString);
        void DetachClient(string clientAddress);
    }
}
