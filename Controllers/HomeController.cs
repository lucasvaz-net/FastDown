using FastDown.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Collections.Generic;
using Grpc.Core;
using MimeKit;


namespace FastDown.Controllers
{
    public class HomeController : Controller
    {

        // Caminho para a pasta que contém os arquivos
         string caminhoPasta = @"C:\Users\TI 03\source\repos\FastDown\wwwroot\arquivos\";



        public ActionResult DownloadArquivos([FromServices] IWebHostEnvironment hostingEnvironment)
        {
            // Obter todos os arquivos na pasta
            string[] arquivos = Directory.GetFiles(caminhoPasta);

            // Criar um objeto da classe ArquivosEPastasModel
            ArquivosEPastasModel arquivosEPastasModel = new ArquivosEPastasModel();

            // Atribuir o nome da pasta
            arquivosEPastasModel.NomePasta = caminhoPasta;

            // Criar uma lista para armazenar os nomes dos arquivos
            List<string> nomesArquivos = new List<string>();

            // Adicionar o nome de cada arquivo à lista
            foreach (string arquivo in arquivos)
            {
                nomesArquivos.Add(Path.GetFileName(arquivo));
            }

            // Atribuir a lista de nomes de arquivos ao objeto da classe ArquivosEPastasModel
            arquivosEPastasModel.NomesArquivos = nomesArquivos;

            // Retornar uma View com o objeto da classe ArquivosEPastasModel
            return View(arquivosEPastasModel);
        }



        public ActionResult Download([FromServices] IWebHostEnvironment hostingEnvironment, string arquivo)
        {
 
            // Caminho completo do arquivo
            string caminhoArquivo = caminhoPasta + arquivo;

            // Verificar se o arquivo existe
            if (!System.IO.File.Exists(caminhoArquivo))
            {
                return NotFound();
            }

            // Retornar o arquivo para download
            return File(System.IO.File.ReadAllBytes(caminhoArquivo), MimeTypes.GetMimeType(caminhoArquivo), arquivo);
        }


    }
}
