using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATM.DataAccess
{
    public static class AccountRepository
    {
        /// <summary>
        /// For this simulation, balance is a static value for any account.
        /// </summary>
        private static float _balance { get; set; }

        /// <summary>
        /// Attempts to adjust the balance of the user's account
        /// </summary>
        /// <param name="offset">The amount to adjust the user's account by</param>
        /// <returns>True if the transaction was successful. Otherwise, false.</returns>
        public static bool AdjustAccountBalance(float offset)
        {
            if(_balance + offset > 0)
            {
                _balance += offset;
                return true;
            }

            return false;
        }

        public static float GetAccountBalance()
        {
            return _balance;
        }
    }
}
