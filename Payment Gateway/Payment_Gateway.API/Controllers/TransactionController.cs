using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment_Gateway.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
       public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            
        }

        [HttpGet("get-all-transactions")]
        [SwaggerOperation(Summary = "Gets all Transaction in the transaction list")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets all Transaction in the transaction list", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
           IEnumerable<Transaction> response = await _transactionService.GetAllTransactions();  
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("get-transactions-by-Id")]
        [SwaggerOperation(Summary = "Gets transaction with id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets transaction with id", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid Id", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction(int Id)
        {
            Transaction response = await _transactionService.GetTransactions(Id);
            return Ok(response);
        }

        [HttpGet("get-transactions-by-reference")]
        [SwaggerOperation(Summary = "Gets transaction with reference")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionByReference(string  reference)
        {
            var transaction = await _transactionService.GetTransactionByReference(reference);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        [HttpGet("date")]
        [SwaggerOperation(Summary = "Gets transaction with date")]
        public async Task<IActionResult> GetTransactionsByDate(DateTime date)
        {
            var transactions = await _transactionService.GetTransactionsByDate(date);
            var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
            return Ok(transactionDtos);
        }
    }
}
