using Microsoft.AspNetCore.Mvc;
using projeto.db;
using projeto.Models;
namespace projeto.Controllers
{
				[ApiController]
    public class ClimaController : ControllerBase
    {
        Consultas _consultas;
        public ClimaController(Consultas consultas)
        {
            _consultas = consultas;
        }

        [HttpPost, Route("/clima/{temperatura}/{umidade}")]
        public IActionResult Post(float temperatura, float umidade)
        {
            try
            {
                if (temperatura == 0 || umidade == 0)
                {
                    return BadRequest(new
                    {
                        mensagem = "Dados incorretos!",
                        temperatura = temperatura,
                        umidade = umidade
                    });
                }
                Clima clima = new Clima
                {
                    Umidade = umidade,
                    Temperatura = temperatura,
                    Data = DateTime.Now
                };
                _consultas.SetClima(clima);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error= ex.Message} );
            }
        }
    }
}