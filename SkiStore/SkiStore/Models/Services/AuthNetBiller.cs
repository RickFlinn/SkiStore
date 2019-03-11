using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Microsoft.Extensions.Configuration;
using SkiStore.Models.Interfaces;

namespace SkiStore.Models.Services
{
    public class AuthNetBiller : IChargeCreditCards
    {
        private readonly IConfiguration _configuration;

        public AuthNetBiller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Attempts to charge a credit card with the given information for the given amount.
        ///      Returns a boolean indicating whether the transaction was successful. 
        /// </summary>
        /// <param name="ccNumber"> Card number of a credit card </param>
        /// <param name="expires"> Expiration date of the credit card  </param>
        /// <param name="securityCode"> Security code of the credit card </param>
        /// <param name="amount"> Amount to charge to the card </param>
        /// <returns>  The response returned by the Auth.Net API when a charge with the given credentials is attempted
        /// </returns>
        public Tuple<bool, string> ChargeCard(string ccNumber, string expires, string secCode, IEnumerable<CartEntry> cartEntries)
        {

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = _configuration["AuthNetApiLoginID"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _configuration["AuthNetTransactionKey"]
            };

            creditCardType creditCard = new creditCardType
            {
                cardNumber = ccNumber,
                expirationDate = expires,
                cardCode = secCode
            };

            paymentType paymentType = new paymentType
            {
                Item = creditCard
            };


            decimal total = 0;
            int index = 0;
            lineItemType[] lineItems = new lineItemType[cartEntries.Count()];
            foreach (CartEntry entry in cartEntries)
            {
                lineItemType item = new lineItemType
                {
                    itemId = entry.Product.ID.ToString(),
                    name = entry.Product.Name,
                    quantity = entry.Quantity,
                    unitPrice = entry.Product.Price
                };

                total += entry.Quantity * entry.Product.Price;

                lineItems[index] = item;
                index++;
            }

            transactionRequestType transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                lineItems = lineItems,
                amount = total,
                payment = paymentType,
            };

            createTransactionRequest request = new createTransactionRequest
            {
                transactionRequest = transactionRequest
            };

            createTransactionController controller = new createTransactionController(request);

            controller.Execute();

            var response = controller.GetApiResponse();

            StringBuilder sb = new StringBuilder();

            

            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {

                        sb.AppendLine($"Successfully created transaction with Transaction ID: {response.transactionResponse.transId}");
                        sb.AppendLine($"Response Code: {response.transactionResponse.responseCode}");
                        sb.AppendLine($"Message Code: {response.transactionResponse.messages[0].code}");
                        sb.AppendLine($"Description: {response.transactionResponse.messages[0].description}");
                        sb.AppendLine($"Success, Auth Code : {response.transactionResponse.authCode}");
                        return new Tuple<bool, string>(true, sb.ToString());
                    }
                    else
                    {
                        sb.AppendLine("Failed transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            sb.AppendLine($"Error message: { response.transactionResponse.errors[0].errorText}");
                        }
                        return new Tuple<bool, string>(false, sb.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        sb.AppendLine($"Error message: { response.transactionResponse.errors[0].errorText}");
                    }
                    else
                    {
                        sb.AppendLine($"Error message: {response.messages.message[0].text}");
                    }
                    return new Tuple<bool, string>(false, sb.ToString());
                }
            }
            else
            {
                return new Tuple<bool, string>(false, "Payment authorization could not be accessed. Please try again later.");
            }
        }
    }
}
