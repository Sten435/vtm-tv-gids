using System;
using System.Net.Http;
using Domein;
using Newtonsoft.Json.Linq;

namespace Model
{
    public class Api
    {
        public static async Task<string> Fetch(string baseUrl)
        {
            using HttpClient client = new();
            using HttpResponseMessage res = await new HttpClient().GetAsync(baseUrl);
            using HttpContent content = res.Content;
            return await content.ReadAsStringAsync();
        }

        public static List<Programma> ParseData(string input)
        {
            var lijstProgrammas = new List<Programma>();
            var rObject = Api.ParseObject(input);
            var rArray = Api.ParseArray(rObject["channels"].ToString());

            for (int i = 0; i < rArray.Count; i++)
            {
                var zender = rArray[i]["name"].ToString();
                var dArray = Api.ParseArray(rArray[i]["broadcasts"].ToString());
                for (int j = 0; j < dArray.Count; j++)
                {
                    var data = Api.ParseObject(dArray[j].ToString());
                    var to = Programma.UnixToDate(long.Parse(data["to"].ToString()));
                    var from = Programma.UnixToDate(long.Parse(data["from"].ToString()));

                    var rerun = bool.Parse(data["rerun"].ToString());
                    var live = bool.Parse(data["live"].ToString());
                    var uuid = data["uuid"].ToString();
                    var titel = data["title"].ToString();
                    var beschrijving = data["synopsis"].ToString();
                    var genre = data["genre"].ToString();
                    var image = data["imageUrl"].ToString();
                    var imageFormat = data["imageFormat"].ToString();
                    var type = data["playableType"].ToString();
                    var duration = data["duration"].ToString();

                    var landVanAfkomst = new List<ProductieLand>();
                    var pCountries = Api.ParseArray(data["productionCountries"].ToString());
                    for (int k = 0; k < pCountries.Count; k++)
                    {
                        var pl = new ProductieLand(pCountries[k]["code"].ToString(),
                            pCountries[k]["name"].ToString());
                        landVanAfkomst.Add(pl);
                    }

                    var p = new Programma(zender, uuid, from, to, titel, rerun, live, beschrijving, genre,
                        duration, image, imageFormat, type, landVanAfkomst);
                    lijstProgrammas.Add(p);
                }
            }

            return lijstProgrammas;
        }

        public static JArray ParseArray(string unparsedData)
        {
            return JArray.Parse(unparsedData);
        }

        public static JObject ParseObject(string unparsedData)
        {
            return JObject.Parse(unparsedData);
        }
    }
}