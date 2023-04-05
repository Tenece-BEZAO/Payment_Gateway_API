using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment_Gateway.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            
        }

        [HttpGet("get-all-to-do-items")]
        [SwaggerOperation(Summary = "Gets all Transaction in the transaction list")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets all Transaction in the transaction list", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllToDoItems()
        {
           IEnumerable<Transaction> response = await _transactionService.GetAllTransactions();  
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("get-to-do-item")]
        [SwaggerOperation(Summary = "Gets item with id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets item with id", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetToDoItem(int Id)
        {
            Transaction response = await _transactionService.GetTransactions(Id);
            return Ok(response);
        }
    }
}
