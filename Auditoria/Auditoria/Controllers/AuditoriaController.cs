using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auditoria.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auditoria.Controllers
{
    [Route("v1/api/auditoria")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        List<Produto> produtos = new List<Produto>
        {
            new Produto { Codigo =1, Nome = "Livro A", Quantidade =5, Tipo = "Ação" },
            new Produto { Codigo =2, Nome = "Livro B", Quantidade =10,  Tipo = "Ação" },
            new Produto { Codigo =3, Nome = "Livro C", Quantidade =15, Tipo = "Aventura" },
            new Produto { Codigo =4, Nome = "Livro D", Quantidade =20,  Tipo = "Estudo" }
        };

        [Route("produtos/{codigoProduto}")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> ConsultaEstoque(int codigoProduto, [FromQuery]int quantidade)
        {
            return Ok(produtos.Where(a => a.Codigo == codigoProduto && a.Quantidade > quantidade).Any());
        }

        [Route("produtos/{codigoProduto}")]
        [HttpPut]
        public ActionResult<IEnumerable<string>> AlteraEstoque(int codigoProduto, [FromQuery]int quantidade)
        {
            var prod = produtos.Where(a => a.Codigo == codigoProduto).FirstOrDefault();

            var response = Response.StatusCode = StatusCodes.Status402PaymentRequired;

            

            if (prod.Quantidade < quantidade)
                return BadRequest(new ApplicationException("Quantidade insuficiente no estoque"));

            prod.Quantidade = prod.Quantidade - quantidade;
            return Ok();
        }



    }
}