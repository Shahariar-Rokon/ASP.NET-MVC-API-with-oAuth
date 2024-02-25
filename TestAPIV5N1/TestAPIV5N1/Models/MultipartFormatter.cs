using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace TestAPIV5N1.Models
{
    public class MultipartFormatter : MediaTypeFormatter
    {
        public MultipartFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }
        public override bool CanReadType(Type type)
        {
            return type==typeof(ReserveRequest);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }
        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var multipart = await content.ReadAsMultipartAsync();
            var resdata = new ReserveRequest();
            foreach(var item in multipart.Contents) 
            {
                var fieldname = item.Headers.ContentDisposition.Name.Trim('\"');
                if (fieldname == "Reservation")
                {
                    var res = await item.ReadAsStringAsync();
                    resdata.Reservation = JsonConvert.DeserializeObject<Reservation>(res);
                }
                else if (fieldname == "ImageFile")
                {
                    resdata.ImageFile = await item.ReadAsByteArrayAsync();
                    resdata.ImageFileName = item.Headers.ContentDisposition.FileName;
                }
            }
            return resdata;
            
        }
    }
}