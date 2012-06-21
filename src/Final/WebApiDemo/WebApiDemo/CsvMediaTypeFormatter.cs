using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace WebApiDemo
{
    public class CsvMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public CsvMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override void WriteToStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders)
        {
            using (var writer = new StreamWriter(stream))
            {
                var list = value as IEnumerable;
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        WriteItem(item, writer);
                    }
                }
                else
                {
                    WriteItem(value, writer);
                }
            }
            stream.Close();
        }

        private void WriteItem(object item, StreamWriter writer)
        {
            var properties = item.GetType().GetProperties();
            var line = string.Join(",", properties.Select(p => Convert.ToString(p.GetValue(item))).ToArray());
            writer.WriteLine(line);
        }
    }
}