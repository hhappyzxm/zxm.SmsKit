using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using zxm.SmsKit.Data;
using Newtonsoft.Json;

namespace zxm.SmsKit
{
    public class ClickatellClient : ISmsClient
    {
        #region Urls

        private const string SendMessageURL = "https://api.clickatell.com/rest/message";

        #endregion

        #region Private Properties

        private readonly RESTCredentials _credentials;

        #endregion

        #region Constructor
        public ClickatellClient(RESTCredentials credentials)
        {
            //Sets the REST API credentials
            _credentials = credentials;
        }

        #endregion

        #region Public Methods

        public async Task<SendMessageResponse> SendMessageAsync(SendMessageRequest request)
        {
            using (var client = new HttpClient())
            {
                var requestJson = JsonConvert.SerializeObject(
                    new Data.Json.MessageRequest.Rootobject
                    {
                        text = request.Message,
                        to = request.PhoneNumbers
                    });

                var responseMessage = await client.SendAsync(GetHttpRequestMessage(SendMessageURL, HttpMethod.Post, new StringContent(requestJson)));
            }
            //try
            //{
            //    client = GetHttpClient();

            //    await client.PostAsync(SendMessageURL,);
            //    //Gets the WebRequest with the SendMessageURL and POST method 
            //    var webRequest = GetWebRequest(Properties.RESTSettings.Default.SendMessageURL, Properties.RESTSettings.Default.PostMethod);

            //    //Creates a JSON object with the message,telephone numbers applied and serialize it to be sent to the Clickatell service
            //    var postData = JsonSerializer<Data.JSON.REST.MessageRequest.Rootobject>(
            //        new Data.JSON.REST.MessageRequest.Rootobject
            //        {
            //            text = request.Message,
            //            to = request.PhoneNumbers
            //        });

            //    //Gets the encoding of the JSON post data created (iso-8859-1)
            //    var bytes = Encoding.GetEncoding(Properties.RESTSettings.Default.EncodingName).GetBytes(postData);
            //    webRequest.ContentLength = bytes.Length;

            //    //Get webRequest Requested stream from encoded bytes
            //    using (var writeStream = webRequest.GetRequestStream())
            //    {
            //        writeStream.Write(bytes, 0, bytes.Length);
            //    }

            //    //Get WebResponse from Clickatell service
            //    var webResponse = GetWebResponse(webRequest);

            //    //Deserlialize the webResponse in JSON and maps the results for the Messages response
            //    var jsonMessages = JsonDeserialize<Data.JSON.REST.MessageResponse.Rootobject>(webResponse.Result).data.message.Select(message => new Message
            //    {
            //        APIMessageID = message.apiMessageId,
            //        To = message.to
            //    }).ToArray();

            //    return new SendMessageResponse
            //    {
            //        Result = webResponse.Result,
            //        Success = webResponse.Success,
            //        Messages = jsonMessages
            //    };
            //}
            //catch (Exception exception)
            //{
            //    return new SendMessageResponse
            //    {
            //        Result = string.Format("Error occured during Clickatell {0}. Details: {1}", MethodBase.GetCurrentMethod().Name, exception.Message),
            //        Success = false
            //    };
            //}
            //finally
            //{
            //    if(client != null)
            //    {
            //        client.Dispose();
            //    }
            //}
        }

        #endregion

        #region Private Methods

        private HttpRequestMessage GetHttpRequestMessage(string url, HttpMethod method, HttpContent content = null)
        {
            var request = new HttpRequestMessage();

            request.Headers.Add("Authorization", $"bearer {_credentials.AuthenticationToken}");
            request.Headers.Add("X-Version", "1");
            request.Headers.Add("ContentType", "application/json");
            request.Headers.Add("Accept", "application/json");

            request.RequestUri = new Uri(url);
            request.Method = method;
            request.Content = content;

            return request;
        }

        //private WebResponse GetWebResponse(HttpWebRequest webRequest)
        //{
        //    //Gets the WebResponse from Clickatell service
        //    using (var response = (HttpWebResponse)webRequest.GetResponse())
        //    {
        //        using (var responseStream = response.GetResponseStream())
        //        {
        //            if (responseStream != null)
        //            {
        //                using (var reader = new StreamReader(responseStream))
        //                {
        //                    //Validate StatusCode returned from Clickatell service
        //                    if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
        //                        throw new Exception(String.Format("Request failed. Received HTTP {0}", response.StatusCode));

        //                    return new Data.WebResponse
        //                    {
        //                        Result = reader.ReadToEnd(),
        //                        Success = true,
        //                        StatusCode = response.StatusCode
        //                    };
        //                }
        //            }
        //            return new Data.WebResponse
        //            {
        //                Result = string.Empty,
        //                Success = false,
        //            };
        //        }
        //    }
        //}

        #endregion
    }
}
