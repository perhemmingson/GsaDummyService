using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Mindscape.Raygun4Net.Builders;
using Mindscape.Raygun4Net.Messages;
using Mindscape.Raygun4Net.WebApi;
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
            var context = new HttpContextWrapper(HttpContext.Current);
            var site = context.Request["site"] as string ?? "unknown";
            var requestMessage = RaygunRequestMessageBuilder.Build(System.Web.HttpContext.Current.Request, null);

            new RaygunWebApiClient().SendInBackground(new RaygunMessage
            {
                OccurredOn = DateTime.UtcNow,
                Details = new RaygunMessageDetails
                {
                    UserCustomData = new List<string> { site }.ToDictionary(x => x),
                    Request = requestMessage,
                    GroupingKey = site,
                    Tags = new List<string> { "GSA" },
                    Error = new RaygunErrorMessage { Message = site }
                }
            });

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