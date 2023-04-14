using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using semana10_Exercicios.Context;
using semana10_Exercicios.DTO;
using semana10_Exercicios.Models;

namespace semana10_Exercicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly LocacaoContext _context;

        public MarcaController(LocacaoContext context)
        {
            _context = context;
        }

        [HttpPost("CriarMarca")]
        public ActionResult Criar([FromBody] MarcaDTO marcaDto)
        {
            if (marcaDto.Codigo != 0)
            {
                return BadRequest("Codigo deve ser ZERO!");
            }
            MarcaModel marcaModel = new MarcaModel();
            marcaModel.Nome = marcaDto.Nome;
            _context.Marcas.Add(marcaModel);
            _context.SaveChanges();
            return Ok("Marca Criada");
        }

        [HttpPut("AtualizarMarca/{id}")]
        public ActionResult Atualizar([FromBody] MarcaDTO marcaDto, int id)
        {
            var existe = _context.Marcas.Find(id);
            if (existe != null)
            {
                if (marcaDto.Codigo != 0)
                {
                    return BadRequest("Não foi possivel alterar!");
                }
                else
                {
                    existe.Id = id;
                    existe.Nome = marcaDto.Nome;
                    _context.Marcas.Update(existe);
                    _context.SaveChanges();
                    return Ok("Alterações foram salvas!");
                }
            }
            return BadRequest();
        }

        [HttpDelete("ExcluirMarca/{id}")]
        public ActionResult Deletar(int id)
        {
            var existe = _context.Marcas.Find(id);
            if (existe != null)
            {
                _context.Marcas.Remove(existe);
                _context.SaveChanges();
                return Ok("Registro removido!");
            }
            return BadRequest("Não foi possivel encontrar este registro, verifique o ID informado!");
        }

        [HttpGet("MostrarTodos")]
        public ActionResult Get()
        {
            List<MarcaDTO> marcas = new List<MarcaDTO>();
            foreach (var item in _context.Marcas)
            {
                MarcaDTO marca = new MarcaDTO();
                marca.Codigo = item.Id;
                marca.Nome = item.Nome;
                marcas.Add(marca);
            }
            return Ok(marcas);
        }

        [HttpGet("MostrarPorId/{id}")]
        public ActionResult Get(int id)
        {
            var existe = _context.Marcas.Find(id);
            if (existe != null)
            {
                MarcaDTO marca = new MarcaDTO();
                marca.Codigo = existe.Id;
                marca.Nome = existe.Nome;
                return Ok(marca);
            }
            return BadRequest("Não foi possivel localizar o ID!");
        }
    }
}
