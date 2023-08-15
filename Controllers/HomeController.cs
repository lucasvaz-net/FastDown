using FastDown.Models;
using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;


namespace FastDown.Controllers
{
    public class HomeController : Controller
    {
        private readonly string ftpServer = "win5113.site4now.net";
        private readonly string ftpUser = "lucasvaz-001";
        private readonly string ftpPassword = "Vitoriade10."; // Replace with your FTP password

        public ActionResult DownloadArquivos()
        {
            List<string> fileNames = new List<string>();

            using (FtpClient client = new FtpClient(ftpServer, ftpUser, ftpPassword))
            {
               
                client.Connect();

                // Get files from the specified directory
                var items = client.GetListing("/lucasvaz/FTP");

                fileNames = items.Where(i => i.Size > 0)  // geralmente, pastas têm tamanho 0
                  .Select(i => i.Name)
                  .ToList();
            }
            var model = new ArquivosEPastasModel { NomesArquivos = fileNames };

            return View(model);  // Passe a instância do modelo para a View
        }
        public ActionResult Download(string arquivo)
        {
          

            byte[] fileData;

            using (FtpClient client = new FtpClient(ftpServer, ftpUser, ftpPassword))
            {
                client.Connect();

                using (MemoryStream ms = new MemoryStream())
                {
                    string fullPath = "/lucasvaz/FTP/" + arquivo;
                    using (Stream ftpStream = client.OpenRead(fullPath))

                    {
                        ftpStream.CopyTo(ms);
                    }
                    fileData = ms.ToArray();
                }
            }

            return File(fileData, "application/octet-stream", arquivo);
        }




    }
}
