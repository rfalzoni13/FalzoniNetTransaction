using FalzoniNetTransaction.Model;
using FalzoniNetTransaction.Service;
using Microsoft.AspNetCore.Mvc;

namespace FalzoniNetTransaction.Api.Controllers;

/// <summary>
/// Endpoint de transação.
/// </summary>
/// <remarks>Este controlador manipula solicitações HTTP relacionadas a operações de transação, como recebimento de transações.</remarks>
[Route("api/transacao")]
[Tags("Transação")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _service;
    private readonly ILogger<TransactionController> _logger;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="TransactionController"/>
    /// service.
    /// </summary>
    /// <param name="service">Parâmetro do service.</param>
    public TransactionController(TransactionService service)
    {
        _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<TransactionController>();
        _service = service;
    }

    /// <summary>
    /// Registrar transação
    /// </summary>
    /// <remarks>Endpoint que grava uma nova transação</remarks>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="422">Unprocessable Entity</response>
    /// <param name="transaction">Objeto de transação</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public IActionResult Receive([FromBody] Transaction? transaction)
    {
        try
        {
            _logger.LogInformation("Recebendo transação");

            _service.Receive(transaction!);

            _logger.LogInformation("Transação recebida com sucesso");

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex.Message);
            return new ContentResult 
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }

    /// <summary>
    /// Remover transações
    /// </summary>
    /// <remarks>Endpoint que remove todas as transações registradas</remarks>
    /// <response code="200">Success</response>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete()
    {
        _logger.LogInformation("Iniciando requisição de limpeza de transações");
        _service.Clear();
        _logger.LogInformation($"Transações removidas");
        return Ok();   
    }
}