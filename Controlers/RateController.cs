using MiniProj_HongSun.Models;
using MiniProj_HongSun.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniProj_HongSun.Controlers
{
    public class RateController
    {
        private AppDbContext _context = new AppDbContext();

        public async Task<bool> FetchOnlineRates()
        {
            const string API_PATH = "https://freecurrencyapi.net/api/v2/latest";
            const string API_KEY = "b573c960-7387-11ec-a4ac-c7e6d1b7a0af";
            const int TIMEOUT = 5000;
            Dictionary<string, double> rates = null;

            // make http request
            string api_url = API_PATH + "?apikey=" + API_KEY;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api_url);
            req.Method = "GET";
            req.Timeout = TIMEOUT;
            req.ContentType = "text/html;charset=UFT-8";

            // read http response
            try
            {
                HttpWebResponse res = (HttpWebResponse)await req.GetResponseAsync();
                if (res != null && res.StatusCode == HttpStatusCode.OK)
                {
                    Stream resStream = res.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream, Encoding.GetEncoding("utf-8"));
                    string data = reader.ReadToEnd();
                    resStream.Close();
                    reader.Close();

                    // map json to dictionary 
                    Dictionary<string, JsonElement> firstLayer = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(data);
                    rates = JsonSerializer.Deserialize<Dictionary<string, double>>(firstLayer["data"].ToString());

                    // write data to DB
                    List<Rate> rateList = new()
                    {
                        new Rate(Location.Denmark, rates["DKK"]),
                        new Rate(Location.Sweden, rates["SEK"]),
                        new Rate(Location.Norway, rates["NOK"]),
                        new Rate(Location.Finland, rates["EUR"]),
                        new Rate(Location.Others, 1)
                    };

                    // If NOT exist insert, if exist update
                    foreach (Rate rate in rateList)
                    {
                        if (this._context.Rates.Where(dbRate => dbRate.RateLocation == rate.RateLocation).ToArray().Length == 0)
                            this._context.Rates.Add(rate);
                        else
                            this._context.Rates.Where(dbRate => dbRate.RateLocation == rate.RateLocation).First<Rate>().RateValue = rate.RateValue;
                    }
                    this._context.SaveChanges();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }

        }

        public double GetRate(Location location)
        {
            return this._context.Rates.Where(rate => rate.RateLocation == location).First<Rate>().RateValue;
        }
    }
}
