using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Feelknit.iOS
{
    public class JsonHttpClient
    {
        private string url;

        public JsonHttpClient(string apiUrl)
        {
			url = apiUrl;
        }

        public async Task<string> PostRequest<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.ContentLength = json.Length;
            using (var webStream = request.GetRequestStream())
            using (var requestWriter = new StreamWriter(webStream, Encoding.ASCII))
            {
                requestWriter.Write(json);
            }
            var response = "";
            try
            {
                var webResponse =  await request.GetResponseAsync();
                using (var webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (var responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                            //Console.Out.WriteLine(response);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }
            return response;
        }

        public async Task<string> GetRequest()
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            //using (var webStream = request.GetRequestStream())
            //using (var requestWriter = new StreamWriter(webStream, Encoding.ASCII))
            //{
            //    requestWriter.Write(json);
            //}
            var response = "";
            try
            {
                var webResponse = await request.GetResponseAsync();
                using (var webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (var responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                            //Console.Out.WriteLine(response);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }
            return response;
        }
    }
}