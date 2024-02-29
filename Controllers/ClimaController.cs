using Microsoft.AspNetCore.Mvc;
using projeto.db;
using projeto.Models;
namespace projeto.Controllers
{
    public class ClimaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] float temperatura, float umidade)
        {
            try
            {
                Console.WriteLine("temperatura: " + temperatura);
                Console.WriteLine("umidade: " + umidade);
                Consultas consultas = new Consultas();
                await consultas.SetData(temperatura, umidade);
                return Ok("Dados inseridos!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}