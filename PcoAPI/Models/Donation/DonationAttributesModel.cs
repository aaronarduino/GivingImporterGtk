using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Models.Donation
{
    public class DonationAttributesModel {
        [JsonProperty("amount_cents")]
        public int? AmountCents {get; set;}
        [JsonProperty("amount_currency")]
        public string AmountCurrency {get; set;}
        [JsonProperty("completed_at")]
        public DateTime? CompletedAt {get; set;}
        [JsonProperty("created_at")]
        public DateTime? CreatedAt {get; set;}
        [JsonProperty("fee_cents")]
        public int? FeeCents {get; set;}
        [JsonProperty("fee_currency")]
        public string FeeCurrency {get; set;}
        [JsonProperty("payment_brand")]
        public string PaymentBrand {get; set;}
        [JsonProperty("payment_check_dated_at")]
        public DateTime? PaymentCheckDatedAt {get; set;}
        [JsonProperty("payment_check_number")]
        public int? PaymentCheckNumber {get; set;}
        [JsonProperty("payment_last4")]
        public string PaymentLast4 {get; set;}
        [JsonProperty("payment_method")]
        public string PaymentMethod {get; set;}
        [JsonProperty("payment_method_sub")]
        public string PaymentMethodSub {get; set;}
        [JsonProperty("payment_status")]
        public string PaymentStatus {get; set;}
        [JsonProperty("received_at")]
        public DateTime ReceivedAt {get; set;}
        [JsonProperty("refundable")]
        public bool? Refundable {get; set;}
        [JsonProperty("refunded")]
        public bool? Refunded {get; set;}
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt {get; set;}
    }
}