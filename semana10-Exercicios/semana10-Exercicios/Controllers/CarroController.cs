using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using semana10_Exercicios.Context;
using semana10_Exercicios.DTO;
using semana10_Exercicios.Models;

namespace semana10_Exercicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly LocacaoContext _context;

        public CarroController(LocacaoContext context)
        {
            _context = context;
        }

        [HttpPost("SalvarCarro")]
        public ActionResult Salvar([FromBody] CarroDTO carro)
        {
            if (carro.Codigo == 0)
            {
                foreach (var item in _context.Marcas)
                {
                    if (item.Nome == carro.DescricaoMarca)
                    {
                        CarroModel carroModel = new CarroModel
                        {
                            Id = carro.Codigo,
                            Nome = carro.DescricaoCarro,
                            DataLocacao = carro.DataLocacao,
                            Marcas = item,

                        };
                        _context.Carros.Add(carroModel);
                        _context.SaveChanges();
                        return Ok("Carro salvo no sistema!");
                    }
                }
            }
            else
            {
                return BadRequest("Codigo deve ser ZERO!");
            }
            return BadRequest("Marca não está registrada!");
        }

        [HttpPut("AlterarCarro")]
        public ActionResult Alterar([FromBody] CarroDTO carro)
        {
            var existe = _context.Carros.Find(carro.Codigo);
            if (existe != null)
            {
                foreach (var item in _context.Marcas)
                {
                    if (item.Nome == carro.DescricaoMarca)
                    {
                        CarroModel carroModel = new CarroModel
                        {
                            Id = carro.Codigo,
                            Nome = carro.DescricaoCarro,
                            DataLocacao = carro.DataLocacao,
                            Marcas = item
                        };
                        _context.Carros.Attach(carroModel);
                        _context.SaveChanges();
                        return Ok("Carro salvo no sistema!");
                    }
                }
            }
            return BadRequest("Não foi possivel alterar este registro!");
        }

        [HttpDelete("DeletarCarro/{id}")]
        public ActionResult Deletar(int id)
        {
            var existe = _context.Carros.Find(id);
            if (existe != null)
            {
                _context.Remove(existe);
                _context.SaveChanges();
                return Ok("Carro deletado do registro!");
            }
            return BadRequest("Não existem registros cadastrados com este Id!");
        }

        [HttpGet("MostrarTodosCarros")]
        public ActionResult Get()
        {
            List<CarroDTO> listaCarros = new List<CarroDTO>();
            foreach (var carro in _context.Carros)
            {
                CarroDTO carroDto = new CarroDTO
                {
                    Codigo = carro.Id,
                    DescricaoCarro = carro.Nome,
                    DataLocacao = carro.DataLocacao,
                };

                List<MarcaDTO> listaMarca = new List<MarcaDTO>();
                foreach (var marcas in _context.Marcas)
                {
                    if (marcas.Id == carro.IdMarca)
                    {
                        carroDto.DescricaoMarca = marcas.Nome;
                        carroDto.CodigoMarca = marcas.Id;
                    }
                }
                listaCarros.Add(carroDto);
            }
            return Ok(listaCarros);
        }

        [HttpGet("MostrarPorId/{id}")]
        public ActionResult Get(int id)
        {
            var existe = _context.Carros.Find(id);
            if (existe != null)
            {
                CarroDTO carroDto = new CarroDTO
                {
                    Codigo = existe.Id,
                    DescricaoCarro = existe.Nome,
                    DataLocacao = existe.DataLocacao,
                };
                List<MarcaDTO> listaMarca = new List<MarcaDTO>();
                foreach (var marcas in _context.Marcas)
                {
                    if (marcas.Id == existe.IdMarca)
                    {
                        carroDto.DescricaoMarca = marcas.Nome;
                        carroDto.CodigoMarca = marcas.Id;
                    }
                }
                return Ok(carroDto);
            }
            return BadRequest("Id não existe no banco de dados!");
        }
    }
}
