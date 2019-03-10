using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Interfaces
{
    public interface IChargeCreditCards
    {
        /// <summary>
        ///     Attempts to charge a credit card with the given information for the given amount.
        ///      Returns a boolean indicating whether the transaction was successful. 
        /// </summary>
        /// <param name="cardNumber"> Card number of a credit card </param>
        /// <param name="expires"> Expiration date of the credit card  </param>
        /// <param name="securityCode"> Security code of the credit card </param>
        /// <param name="amount"> Amount to charge to the card </param>
        /// <returns> Boolean indicating whether the card was successfully charged </returns>
        ANetApiResponse ChargeCard(string cardNumber, string expires, IEnumerable<CartEntry> cartEntries);
    }
}
