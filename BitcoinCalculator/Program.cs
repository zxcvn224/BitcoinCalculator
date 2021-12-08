using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BitcoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitcoin = GetRates();

            Console.WriteLine("Enter bitcoin amount: ");
            float userBitcoin = float.Parse(Console.ReadLine());
            Console.WriteLine("'EUR'/'USD'/'GBP':");
            string userCurrency = Console.ReadLine();
            float currentRate = 0;


            if (userCurrency == "EUR")
            {
                currentRate = currentBitcoin.bpi.EUR.rate_float;
            }
            else if (userCurrency == "USD")
            {
                currentRate = currentBitcoin.bpi.USD.rate_float;
            }
            else if (userCurrency == "GBP")
            {
                currentRate = currentBitcoin.bpi.GBP.rate_float;
            }

            float result = currentRate * userBitcoin;
            Console.WriteLine($"You have {userCurrency} {result} of Bitcoin");
            //BitcoinRate currentBitcoin = GetRates();
            //Console.WriteLine($"current rate: {currentBitcoin.bpi.USD.code} {currentBitcoin.bpi.USD.rate_float}");
            //Console.WriteLine($"current rate: {currentBitcoin.bpi.EUR.code} {currentBitcoin.bpi.EUR.rate_float}");
            //Console.WriteLine($"current rate: {currentBitcoin.bpi.GBP.code} {currentBitcoin.bpi.GBP.rate_float}");
            //Console.WriteLine($"{currentBitcoin.disclaimer}");
        }

        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitcoinRate bitcoinData;

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }

            return bitcoinData;
        }
    }
}