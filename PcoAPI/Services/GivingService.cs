using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PcoAPI.Models.Donation;
using PcoAPI.Models.Person;
using PcoAPI.Models.Batch;
using PcoAPI.Models.Fund;
using PcoAPI.Models.PaymentSource;

namespace PcoAPI.Services
{
    public partial class GivingService
    {
        public async Task<DonationsModel> GetDonations(){
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/giving/v2/donations", ApiUrl));
            return await ExecuteGet<DonationsModel>(request);
        }

        public async Task<BatchesModel> GetBatches()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/giving/v2/batches", ApiUrl));
            return await ExecuteGet<BatchesModel>(request);
        }

        public async Task<BatchResultModel> CreateBatch(string description){
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, string.Format("{0}/giving/v2/batches", ApiUrl));
            NewBatchAttributesModel batchAttributes = new NewBatchAttributesModel() { Description = description};
            BatchModel batch = new BatchModel() { PCOType = "Batch", Attributes = batchAttributes};
            string data = JsonConvert.SerializeObject(new {data = batch}, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            request.Content = new StringContent(data);

            return await ExecutePost<BatchResultModel>(request);
        }

        public async Task<bool> CreateDonation(BatchModel batch, NewDonationModel donation)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, string.Format("{0}/giving/v2/batches/{1}/donations", ApiUrl, batch.ID));
            string data = JsonConvert.SerializeObject(donation, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            request.Content = new StringContent(data);

            return await ExecutePost(request);
        }

        public async Task<PersonsModel> SearchForPerson(string firstName, string lastName){
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/giving/v2/people?where[first_name]={1}&where[last_name]={2}", ApiUrl, firstName, lastName));
            return await ExecuteGet<PersonsModel>(request);
        }

        public async Task<FundsModel> SearchForFund(string fundName)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/giving/v2/funds?where[name]={1}", ApiUrl, fundName));
            return await ExecuteGet<FundsModel>(request);
        }

        public async Task<PaymentSourcesModel> GetPaymentSources()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/giving/v2/payment_sources", ApiUrl));
            return await ExecuteGet<PaymentSourcesModel>(request);
        }
    }
}