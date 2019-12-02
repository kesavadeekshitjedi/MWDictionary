using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;


namespace Dictionary_Console_MW
{
    class MWDictionary
    {
        public static string ApiKey="f4ba5600-2e16-4bbb-8655-ddb27d0de8f3";
        public static string BaseURL = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";
        public static HttpWebRequest WebRequest;
        public static HttpWebResponse WebResponse;
        public Stream ResponseStream = null;
        public StreamReader MyStreamReader = null;
        static void Main(string[] args)
        {
            MWDictionary mwd = new MWDictionary();
            Console.WriteLine("Enter the word to get the meaning:");
            string WordID = Console.ReadLine();
            Console.WriteLine(mwd.GetWordDefinition(WordID));

        }
        public string GetWordDefinition(string WordID)
        {
            var WordDefinition = "";
            string FinalURL = BaseURL + WordID + "?key=" + ApiKey;
            //Console.WriteLine(FinalURL);
            WebRequest = (HttpWebRequest)HttpWebRequest.Create(FinalURL);
            WebRequest.Method = WebRequestMethods.Http.Get;
            WebRequest.Accept = "application/json";
            WebResponse = (HttpWebResponse)WebRequest.GetResponse();
            if(WebResponse.StatusCode==HttpStatusCode.OK)
            {
                ResponseStream = WebResponse.GetResponseStream();
                MyStreamReader = new StreamReader(ResponseStream, Encoding.UTF8);
                string TheJSONObject = MyStreamReader.ReadToEnd();
                dynamic TheParsedJSON = JsonConvert.DeserializeObject(TheJSONObject);
                Console.WriteLine(TheParsedJSON);
                var test=TheParsedJSON[0]["shortdef"][0];
                Console.WriteLine(test);
                //Console.WriteLine(TheJSONObject);
            }


            return WordDefinition;
        }
    }
}
