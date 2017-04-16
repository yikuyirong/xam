using Hungsum.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hungsum.Framework.Utilities
{
    public class HsCoreWSHelper
    {
        public string URL { get; set; }

        protected async Task<string> postByName(string name,string data)
        {
            try
            {
                this.showLoading();

                byte[] ds = Encoding.UTF8.GetBytes($"data={data}");

                HttpWebRequest request = WebRequest.CreateHttp($"{(URL.EndsWith("/") ? URL : URL + URL + "/")}{name}");

                request.ContentType = "application/x-www-form-urlencoded";

                request.Method = "POST";

                //request.ContentLength = ds.Length;

                using (Stream stream = await getRequestStream(request))
                {
                    stream.Write(ds, 0, ds.Length);
                }


                using (Stream stream = (await getResponseStream(request)))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        data = sr.ReadToEnd();

                        XElement returnData = XElement.Parse(XElement.Parse(data).Value);

                        data = returnData.Element("Data").Value;

                        if (returnData.Element("Code").Value != "0")
                        {
                            throw new HsException(data);
                        }
                        else
                        {
                            return data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hideLoading();
            }


        }

        protected async Task<Stream> getRequestStream(HttpWebRequest request)
        {
            return await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, TaskContinuationOptions.None);
        }

        protected async Task<Stream> getResponseStream(HttpWebRequest request)
        {
            WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, TaskCreationOptions.None);
            return response.GetResponseStream();
        }

        protected void showLoading()
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("处理中");
        }

        protected void hideLoading()
        {
            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
        }
    }
}
