using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using HeyRed.Mime;


namespace FunctionAppTemplate
{
    public static class Function1
    {
        [FunctionName("WebSite")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(@"www\index.html", FileMode.Open);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(GetMimeType(@"www\index.html"));
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        private static string GetMimeType( string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return MimeTypesMap.GetMimeType(fileInfo.Extension);
        }
    }
}

