using Domein;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Presentatie
{
    public class VtmApp
    {
        static async Task Main(string[] args)
        {
            var date = DateTime.Today.ToString("yyyy-MM-dd");
            var response = await Api.Fetch($"https://vtm.be/tv-gids/api/v2/broadcasts/{date}");

            var lijstProgrammas = Api.ParseData(response);

            foreach (var progma in lijstProgrammas)
            {
                Console.WriteLine(JsonConvert.SerializeObject(progma, Formatting.Indented));
            }
        }
    }
}