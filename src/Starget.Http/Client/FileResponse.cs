using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    public class FileResponse : ApiResponse
    {
        public List<FileContent> Files { get; set; } = new List<FileContent>();

        public FileContent File
        {
            get
            {
                if(this.Files != null && this.Files.Count() > 0)
                {
                    return this.Files[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public override async Task DeserializeMessageAsync(HttpResponseMessage message)
        {
            this.StatusIsSucceed = message.IsSuccessStatusCode;
            this.StatusCode = message.StatusCode;
            this.StatusMessage = message.ReasonPhrase;
            if (this.StatusIsSucceed)
            {
                if (message.Content is ByteArrayContent)
                {
                    var content = message.Content as ByteArrayContent;
                    var c = await GetFileContentAsync(content);
                    this.Files.Add(c);
                }
                else if(message.Content is StreamContent)
                {
                    var content = message.Content as StreamContent;
                    var c = await GetFileContentAsync(content);
                    this.Files.Add(c);
                }
                else if (message.Content is MultipartContent)
                {
                    var mcontent = message.Content as MultipartContent;
                    foreach (var ct in mcontent)
                    {
                        if (ct is ByteArrayContent)
                        {
                            var content = ct as ByteArrayContent;
                            var c = await GetFileContentAsync(content);
                            this.Files.Add(c);
                        }
                        else if (ct is StreamContent)
                        {
                            var content = ct as StreamContent;
                            var c = await GetFileContentAsync(content);
                            this.Files.Add(c);
                        }
                    }
                }
            }
        }

        public async Task<FileContent> GetFileContentAsync(ByteArrayContent content)
        {
            FileContent c = new FileContent();
            var values = content.Headers.GetValues("Name");
            if (values != null && values.Count() > 0)
            {
                c.Name = values.FirstOrDefault();
            }
            values = content.Headers.GetValues("FileName");
            if (values != null && values.Count() > 0)
            {
                c.FileName = values.FirstOrDefault();
            }
            c.Bytes = await content.ReadAsByteArrayAsync();
            return c;
        }

        public async Task<FileContent> GetFileContentAsync(StreamContent content)
        {
            FileContent c = new FileContent();
            c.Name = content.Headers.ContentDisposition?.Name;
            c.FileName = content.Headers.ContentDisposition?.FileName;
            c.Bytes = await content.ReadAsByteArrayAsync();
            return c;
        }
    }
}
