using Newtonsoft.Json;
using NorthwareTimeControl.Business;
using NorthwareTimeControl.Models.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NorthwareTimeControl.Http
{
    public class ApiServiceHttpClient
    {
        #region [ Attributes ]
        #endregion [ Attributes ]

        #region [ Constructor ]
        public ApiServiceHttpClient()
        {

        }
        #endregion [ Constructor ]

        #region [ Methods ]
        public async Task<BaseResponse<T>> GetAsync<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                    Timeout = TimeSpan.FromSeconds(30),
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                string answer = await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new BaseResponse<T>
                    {
                        Successful = false,
                        Errors = new List<string> { answer },
                    };
                }

                T response = JsonConvert.DeserializeObject<T>(answer);
                return new BaseResponse<T>
                {
                    DataResponse = response,
                    Successful = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>
                {
                    Successful = false,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<TOut>> PostAsync<TIn, TOut>(string urlBase, string servicePrefix, string controller, TIn model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                    Timeout = TimeSpan.FromSeconds(60),
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                string answer = await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new BaseResponse<TOut>
                    {
                        Successful = false,
                        Errors = new List<string> { answer },
                    };
                }

                TOut response = JsonConvert.DeserializeObject<TOut>(answer);
                return new BaseResponse<TOut>
                {
                    DataResponse = response,
                    Successful = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TOut>
                {
                    Successful = false,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<TOut>> DeleteAsync<TIn, TOut>(string urlBase, string servicePrefix, string controller, TIn model)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                    Timeout = TimeSpan.FromSeconds(30),
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage responseMessage = await client.DeleteAsync(url);
                string answer = await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new BaseResponse<TOut>
                    {
                        Successful = false,
                        Errors = new List<string> { answer },
                    };
                }

                TOut response = JsonConvert.DeserializeObject<TOut>(answer);
                return new BaseResponse<TOut>
                {
                    DataResponse = response,
                    Successful = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TOut>
                {
                    Successful = false,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        #endregion [ Methods ]
    }
}
