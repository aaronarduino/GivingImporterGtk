using System;
using System.Collections.Generic;
using HCCInfrastructure.Models;
using PcoAPI.Models.Donation;

namespace HCCInfrastructure.Helpers
{
    public static class DonationHelper
    {
        public static DonationDataModel MapCsvRecordToDonationData(BatchFileLineModel batchFileLineModel, Dictionary<string, string> fundMap)
        {
            PcoAPI.Enums.Enums.PaymentMethods paymentMethod = PaymentMethodFromString(batchFileLineModel.PayType);
            int cents = (int)(batchFileLineModel.Amount * 100);

            return new DonationDataModel()
            {
                PaymentMethod = paymentMethod,
                ReceivedAt = batchFileLineModel.BatchDate,
                PersonFirstName = batchFileLineModel.FirstName,
                PersonLastName = batchFileLineModel.LastName,
                AmountCents = cents,
                FundName = TranslateFundName(fundMap, batchFileLineModel.AccountName),
            };
        }

        private static PcoAPI.Enums.Enums.PaymentMethods PaymentMethodFromString(string input)
        {
            switch (input.ToLower())
            {
                case "ach":
                    return PcoAPI.Enums.Enums.PaymentMethods.ach;
                case "card":
                    return PcoAPI.Enums.Enums.PaymentMethods.card;
                case "online":
                    return PcoAPI.Enums.Enums.PaymentMethods.card;
                case "cash":
                    return PcoAPI.Enums.Enums.PaymentMethods.cash;
                case "check":
                    return PcoAPI.Enums.Enums.PaymentMethods.check;
                default:
                    return PcoAPI.Enums.Enums.PaymentMethods.cash;
            }
        }

        private static string TranslateFundName(Dictionary<string, string> fundMap, string fundName)
        {
            if (fundMap.ContainsKey(fundName))
            {
                return fundMap[fundName];
            }
            else
            {
                throw new Exception("Fund name not found in mapping");
            }
        }
    }
}
