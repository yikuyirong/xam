using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        const string API_KEY = "cb6d5cfe5216e4a3af189801c802dba5";

        const string SHORTCUT = "Jbcmp";

        static void Main(string[] args)
        {
            getVersion();

            Console.ReadLine();
        }

        static void getVersion()
        {
            try
            {
                byte[] ds = Encoding.UTF8.GetBytes($"shortcut={SHORTCUT}&_api_key={API_KEY}");

                HttpWebRequest request = WebRequest.CreateHttp("http://www.pgyer.com/apiv1/app/getAppKeyByShortcut");

                request.ContentType = "application/x-www-form-urlencoded";

                request.Method = "POST";

                //request.ContentLength = ds.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(ds, 0, ds.Length);
                }


                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string data = sr.ReadToEnd();

                        JContainer jc = JsonConvert.DeserializeObject(data) as JContainer;

                        Console.WriteLine(jc.Value<int>("code"));

                        Console.WriteLine(jc.Value<string>("message"));

                        Console.WriteLine(jc.Value<string>("abc"));

                        JToken appdata = jc.Value<JToken>("data");

                        string appKey = appdata.Value<string>("appKey");

                        string appversion = appdata.Value<string>("appVersion");

                        Console.WriteLine($"appKey:{appKey} appVersion{appversion}");

                        //jc.First(r=>((JProperty)r).Name == "code").Value()

                        //foreach (JProperty jp in jc)
                        //{
                        //    //Console.WriteLine(jp);

                        //    Console.WriteLine(jp.Name);

                        //    Console.WriteLine(jp.);
                        //}




                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task<Stream> getRequestStream(HttpWebRequest request)
        {
            return await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, TaskContinuationOptions.None);
        }

        static async Task<Stream> getResponseStream(HttpWebRequest request)
        {
            WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, TaskCreationOptions.None);
            return response.GetResponseStream();
        }
    }
}
