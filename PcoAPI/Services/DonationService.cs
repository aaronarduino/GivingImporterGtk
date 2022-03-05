using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PcoAPI.Interfaces;
using PcoAPI.Models;
using PcoAPI.Models.Batch;
using PcoAPI.Models.Designation;
using PcoAPI.Models.Donation;
using PcoAPI.Models.Fund;
using PcoAPI.Models.PaymentSource;
using PcoAPI.Models.Person;
using static PcoAPI.Enums.Enums;

namespace PcoAPI.Services
{
    public class DonationService
    {
        private string PaymentSource { get; set; }
        private string BatchName { get; set; }
        public string BatchDescription { get; private set; }

        private GivingService GivingService;

        public DonationService(string defaultPaymentSource, string batchName, string apiUrl, string clientId, string clientSecret, Action<string> writeToScreen)
        {
            GivingService = new GivingService(apiUrl, clientId, clientSecret, writeToScreen);
            PaymentSource = defaultPaymentSource;
            BatchName = batchName;
            BatchDescription = $"{BatchName} - {DateTime.Now.ToShortDateString()}";
        }

        public async Task<BatchesModel> GetBatches()
        {
            BatchesModel result = await GivingService.GetBatches();
            return result;
        }

        public async Task<BatchResultModel> CreateBatch()
        {
            BatchResultModel batch = await GivingService.CreateBatch(BatchDescription);
            return batch;
        }

        public async Task<NewDonationModel> CreateDonation(DonationDataModel donationDataModel)
        {

            // 1. Find Person:
            PersonsModel persons = await GivingService.SearchForPerson(donationDataModel.PersonFirstName, donationDataModel.PersonLastName);

            if(persons.Data.Count < 1)
            {
                throw new Exception("No person found in giving with that name.");
            }

            PersonModel person = persons.Data.FirstOrDefault();

            // 1.1 Stop creation of donation if person found does not match the person in donationDataModel
            if(person.Attributes.FirstName + person.Attributes.LastName != donationDataModel.PersonFirstName + donationDataModel.PersonLastName)
            {
                // 1.1.1 Names do not match. Write bad record to exception csv file.
                throw new Exception("Name found does not match name in donationDataModel");
            }


            // 2. Find Fund:
            FundsModel funds = await GivingService.SearchForFund(donationDataModel.FundName);
            if (funds.Data.Count < 1)
            {
                throw new Exception("Fund was not found in Giving.");
            }
            FundModel fund = funds.Data.First();

            // 3. Find PaymentSource:
            PaymentSourcesModel paymentSources = await GivingService.GetPaymentSources();
            if (paymentSources.Data.Count < 1)
            {
                throw new Exception("Payment Sources was not found in Giving.");
            }
            PaymentSourceModel paymentSource = paymentSources.Data.Where(p => p.Attributes.Name == PaymentSource).First();

            // 4. Create Designation:
            DesignationAttributesModel designationAttributesModel = new DesignationAttributesModel() { AmountCents = donationDataModel.AmountCents };
            DesignationModel designation = new DesignationModel()
            {
                PCOType = "Designation",
                Attributes = designationAttributesModel,
                Relationships = new DesignationRelationshipModel() { Fund = new SingleDataModel<FundModel>() { Data = fund } },
            };

            // 5. Create Donation:
            return new NewDonationModel()
            {
                Data = new DonationRelationshipsModel()
                {
                    PCOType = "Donation",
                    Attributes = new DonationAttributesModel()
                    {
                        PaymentMethod = donationDataModel.PaymentMethod.ToString(),
                        ReceivedAt = donationDataModel.ReceivedAt,
                    },
                    Relationships = new NewDonationRelationshipsModel
                    {
                        Person = new SingleDataModel<PersonModel>() { Data = person },
                        PaymentSource = new SingleDataModel<PaymentSourceModel>() { Data = paymentSource },
                    },
                },
                Included = new List<DesignationModel>() { designation },
            };
        }

        public async Task<bool> AddToBatch(BatchModel batch, NewDonationModel donation)
        {
            return await GivingService.CreateDonation(batch, donation);
        }
    }
}
