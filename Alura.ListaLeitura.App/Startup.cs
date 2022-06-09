using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    internal class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);

            builder.MapRoute("/Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("/Livros/Lendo", LivrosLendo);
            builder.MapRoute("/Livros/Lidos", LivrosLidos);
            
            var rotas = builder.Build();

            app.UseRouter(rotas);


            app.Run(Roteameneto);



        }

        public Task Roteameneto(HttpContext context) 
        {
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
             {
                 {"/Livros/ParaLer", LivrosParaLer },
                 {"/Livros/Lendo", LivrosLendo },
                 {"/Livros/Lidos", LivrosLidos  },

             };
                
            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminho inexistente.");



        }

        // objeto context que é do tipo HttpContext ele contém toda
        // informação necessária sobre uma requisição que está chegando
        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

           return context.Response.WriteAsync(_repo.ParaLer.ToString());         
        }

        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }


    }
}