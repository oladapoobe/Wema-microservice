using System;
using System.Collections.Generic;
using System.Text;

namespace TestCustomer
{
    public static class Model
    {
        public class CustomerModel
        {
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Pasword { get; set; }
            public string State { get; set; }
            public string Lga { get; set; }
        }

        public class OTPModel
        {
            public string PhoneNumber { get; set; }
        }

        public class SMS
        {
            public string PhoneNumber { get; set; }
            public string Body { get; set; }
        }

        public class SmsSettings
        {
            public string XRapidAPIKey { get; set; }
            public string XRapidAPIHost { get; set; }
            public string user { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string sms { get; set; }
            public string method { get; set; }
            public string classvalue { get; set; }
            public string password { get; set; }
            public string RequestUri { get; set; }
        }

        public class VerifyOTPModel
        {
            public string PhoneNumber { get; set; }
            public string OTP { get; set; }
        }

        public class Customer
        {
            
            public int Id { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Pasword { get; set; }
            public string State { get; set; }
            public string Lga { get; set; }
        }

        public class LocalGovernment
        {
           
            public long Id { get; set; }
            public long State_id { get; set; }
            public string Name { get; set; }
        }

        public class OtpLog
        {
           
            public int Id { get; set; }
            public string PhoneNumber { get; set; }
            public string Otp { get; set; }
            public System.DateTime DateCreated { get; set; }
            public System.DateTime DateExpired { get; set; }
        }

        public class State
        {
          
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class JsonResponseResult
        {
            public bool IsSuccessful { get; set; }
            public string Message { get; set; }
        }
    }
}
