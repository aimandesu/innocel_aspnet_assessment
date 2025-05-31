using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using api.Extensions;
using api.Dtos.Transaction.Result;
using api.Dtos.Transaction;
using System.Text;

namespace api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public TransactionRepository(
            UserManager<User> userManager,
              SignInManager<User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<SuccessTransactionDto>> ProcessTransactionSequence(TransactionDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.PartnerKey);

            if ((user?.UserName ?? "") != dto.PartnerKey)
            {
                return Result<SuccessTransactionDto>.Fail("Unauthorized partner or Signature Mismatch");
            }

            if ((dto.PartnerPassword ?? "") != "")
            {
                var decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(dto.PartnerPassword));

                var result = await _signInManager.CheckPasswordSignInAsync(user, decodedPassword, false);

                if (!result.Succeeded)
                    return Result<SuccessTransactionDto>.Fail("Invalid password or Signature mismatch");

            }



            if (!dto.Items.Length.Equals(0))
            {
                var sumUnitPrice = dto.Items.Sum((e) => e.UnitPrice);

                if (dto.TotalAmount != sumUnitPrice)
                {
                    return Result<SuccessTransactionDto>.Fail("Invalid total amount");
                }

            }


            var successDto = new SuccessTransactionDto
            {
                Result = Enum.TransactionResult.Success,
                TotalAmount = 1000,
                TotalDiscount = 100,
                FinalAmount = 900,
                ResultMessage = "Transaction processed successfully"
            };

            return Result<SuccessTransactionDto>.Success(successDto);
        }

    }
}