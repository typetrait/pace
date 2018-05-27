using Pace.Client.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Client.Web
{
    public class WebFileDownloader
    {
        private HttpClient httpClient;

        public WebFileDownloader()
        {
            httpClient = new HttpClient();
        }

        public async void DownloadFile(string url)
        {
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return;

            byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

            string fileName = string.Empty;
            string extension = string.Empty;

            var splitUrl = url.Split('/');

            var possibleFile = splitUrl[splitUrl.Length - 1];

            if (possibleFile.Contains("."))
            {
                var temp = possibleFile.Split('.');

                fileName = temp[0];

                if (temp[1].Contains('?'))
                {
                    temp[1] = temp[1].Split('?')[0];
                }

                extension = temp[1];

                fileName = $"{fileName}.{extension}";
            }
            else
            {
                fileName = possibleFile;
            }

            if (response.Content.Headers.ContentDisposition != null)
            {
                fileName = response.Content.Headers.ContentDisposition.FileName;
            }

            if (response.Content.Headers.ContentType != null)
            {
                extension = MimeTypeMap.GetExtension(response.Content.Headers.ContentType.MediaType);
            }

            File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, fileName), fileBytes);
        }
    }
}