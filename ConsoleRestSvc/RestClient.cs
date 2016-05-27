using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ConsoleRestSvc
{
    class RestClient
    {
        public RestClient()
        { }
        public string GetData(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            //request.Method = "POST";
            request.Method = "GET";
            request.ContentType = "application/json";


            try
            {
                // get the response
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();

                responseReader.Close();
                return response;

            }
            catch (WebException we)
            {
                string webExceptionMessage = we.Message;
                return null;
            }
            catch (Exception ex)
            {
                // no need to do anything special here....
                return null;
            }
        }
        public T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
        public string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }
    }

}
