using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using Mindscape.Raygun4Net.Builders;
using Mindscape.Raygun4Net.Messages;
using Mindscape.Raygun4Net.WebApi;

namespace GsaDummyService.Controllers
{
    public class SearchController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string nullSearchFileName = @"emptygsaxml.xml";
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nullSearchFileName);
            var doc = XDocument.Load(filepath);

            var context = new HttpContextWrapper(HttpContext.Current);
            var site = context.Request["site"] as string ?? "unknown";
            var requestMessage = RaygunRequestMessageBuilder.Build(System.Web.HttpContext.Current.Request, null);
            
            new RaygunWebApiClient().SendInBackground(new RaygunMessage
            {
                OccurredOn = DateTime.UtcNow,
                Details = new RaygunMessageDetails
                {
                    UserCustomData = new List<string>{site}.ToDictionary(x=>x),
                    Request = requestMessage,
                    GroupingKey = site,
                    Tags = new List<string> { "GSA"},
                    Error = new RaygunErrorMessage { Message = site}
                }
            });

            return new HttpResponseMessage
            {
                Content = new StringContent(doc.ToString(), Encoding.UTF8, "application/xml")
            };
        }
    }
}