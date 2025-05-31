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

        public TransactionController(
         ITransactionRepository transactionRepository
        )
        {
            _transactionRepo = transactionRepository;
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

            var dto = transactionDto with { Sig = SignatureHelper.GenerateSignature(transactionDto) }; //sig too 

            var result = await _transactionRepo.ProcessTransactionSequence(dto); //change this to dto

            if (!result.IsSuccess)
            {
                return Unauthorized(new FailTransactionDto
                {
                    Result = Enum.TransactionResult.Failure,
                    ResultMessage = result.ErrorMessage
                });
            }

            return Ok(result.Data);

        }

    }
}