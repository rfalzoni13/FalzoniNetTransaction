using FalzoniNetTransaction.Model;
using FalzoniNetTransaction.Service;
using Microsoft.AspNetCore.Mvc;

namespace FalzoniNetTransaction.Api.Controllers
{
    /// <summary>
    /// Endpoint de estatísticas.
    /// </summary>
    /// <remarks>Este controlador obtém as estatísticas das transações.</remarks>
    [Route("api/estatistica")]
    [Tags("Estatística")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly TransactionService _service;
        private readonly ILogger<StatisticsController> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="StatisticsController"/>
        /// service.
        /// </summary>
        /// <param name="service">Parâmetro do service.</param>
        public StatisticsController(TransactionService service)
        {
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<StatisticsController>();
            _service = service;
        }

        /// <summary>
        /// Calcular estatísticas
        /// </summary>
        /// <remarks>Endpoint que retorna as estatísticas das transações nos últimos 60 segundos</remarks>
        /// <response code="200">Success</response>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStatistics()
        {
            _logger.LogInformation("Iniciando requisição das estatísticas");

            SummaryStatistics statistics = _service.GetStatistics();

            _logger.LogInformation("Estatísticas calculadas com sucesso!");

            return Ok(statistics);   
        }
    }
}
