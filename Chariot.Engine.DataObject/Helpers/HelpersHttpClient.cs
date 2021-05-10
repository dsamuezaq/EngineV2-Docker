
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;

namespace Chariot.Engine.DataObject.Helpers
{
  public  class HelpersHttpClient
    {

        private string BaseUrl = "http://pedido.gnoboa.com:2108/api/";
        public async Task<List<T>> GetApi<T>(string API) where T : class
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BaseUrl);
                //HTTP GET
                var responseTask = client.GetAsync(API);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<T>>();
                    readTask.Wait();

                    List<T> _data = readTask.Result;
                    return _data;

                }
            }
            return null;
        }
        public async Task<bool> PostApi(string API, string payload)
        {
            using (var client = new HttpClient())
            {
                //  HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseUrl);
                //HTTP GET
                var responseTask = client.PostAsync(API, stringContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<bool>();
                    readTask.Wait();

                    bool _data = readTask.Result;
                    return _data;

                }
            }
            return false;
        }
        public async Task<bool> GettApiParam(string API)
        {
            using (var client = new HttpClient())
            {
                //  HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                //  var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseUrl);
                //HTTP GET
                var responseTask = client.GetAsync(API);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<bool>();
                    readTask.Wait();

                    bool _data = readTask.Result;
                    return _data;

                }
            }
            return false;
        }
    }
}

