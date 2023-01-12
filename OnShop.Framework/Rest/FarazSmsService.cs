using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using OnShop.Framework.Common;
using RestSharp;
using RestSharp.Authenticators;

namespace OnShop.Framework.Rest
{
    public class FarazSmsService
    {
        private readonly string _url;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _from;
        //private readonly RestClient _client;

        public FarazSmsService(string userName, string password, string @from)
        {
            //_url = "http://rest.ippanel.com";
            _url = "https://ippanel.com/api/select";
            _userName = userName;
            //_client = GetRestClient();
            _password = password;
            _from = @from;
        }
        public bool SendSms(string message, string to)
        {
            var client = new RestClient(_url) { Timeout = -1 };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "PHPSESSID=nqd2rmco2rv66u6qvnlp169nt5; DefaultLang=fa");

            var model = new FarazSmsSendModel
            {
                Message = message,
                From = _from,
                Op = "send",
                Pass = _password,
                Uname = _userName,
                To = new[] { to },
                Time = string.Empty
            };
            var json = JsonConvert.SerializeObject(model);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var result = client.Execute(request);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}