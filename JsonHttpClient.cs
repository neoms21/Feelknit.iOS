using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Feelknit
{
    public class JsonHttpClient
    {
        private string url;

        public JsonHttpClient(string apiUrl)
        {
            url = string.Format("http://127.0.0.1/FeelKnitService/{0}", apiUrl);
        }

        public async Task<string> PostRequest<T>(T obj)
        {
            string result;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(obj);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    result = streamReader.ReadToEnd();
                }

                return result;
            }
        }
    }
}