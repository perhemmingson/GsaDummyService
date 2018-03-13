using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;

namespace GsaDummyService.Controllers
{
    public class SuggestController : ApiController
    {

        //public HttpResponseMessage Get()
        //{
        //    string nullSuggestFilename = @"samplejson.txt";

        //    var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nullSuggestFilename);
        //    //return new JsonResult { Data = File.ReadAllText(filepath), ContentType = "application/json" };
        //    return new HttpResponseMessage
        //    {
        //        Content = new StringContent(File.ReadAllText(filepath), Encoding.UTF8, "application/json")
        //    };
        //}

        public string Get()
        {
            var result = new List<Result>(0);
            var model = new RootObject
            {
                Query = string.Empty,
                Results = result
            };

            return JsonConvert.SerializeObject(model);
        }
    }



    public class Result
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class RootObject
    {
        public string Query { get; set; }
        public List<Result> Results { get; set; }
    }


}