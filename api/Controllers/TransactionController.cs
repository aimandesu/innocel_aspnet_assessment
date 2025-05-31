using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Transaction;
using api.Dtos.Transaction.Result;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/submittrxmessage")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionRepository _transactionRepo;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(
         ITransactionRepository transactionRepository,
         ILogger<TransactionController> logger
        )
        {
            _transactionRepo = transactionRepository;
            _logger = logger;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProcessTransaction(
            [FromBody] TransactionDto transactionDto
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // var encodedPassword = string.IsNullOrWhiteSpace(transactionDto.PartnerPassword)
            //     ? string.Empty
            //     : Convert.ToBase64String(Encoding.UTF8.GetBytes(transactionDto.PartnerPassword));

            _logger.LogInformation("Processing transaction for PartnerKey: {PartnerKey}, RefNo: {RefNo}",
            transactionDto.PartnerKey, transactionDto.PartnerRefNo);

            var dto = transactionDto with
            {
                Sig = SignatureHelper.GenerateSignature(transactionDto),
                Timestamp = DateTime.UtcNow.ToString("o")
            };

            var result = await _transactionRepo.ProcessTransactionSequence(dto);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error occurred while processing transaction: {error}", result.ErrorMessage);
                return Unauthorized(new FailTransactionDto
                {
                    Result = Enum.TransactionResult.Failure,
                    ResultMessage = result.ErrorMessage
                });
            }

            _logger.LogInformation("Transaction processed completed: {@RequestBody}", result.Data);
            return Ok(result.Data);

        }

    }
}