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
using api.Helpers;

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
                var decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(dto?.PartnerPassword ?? ""));

                var result = await _signInManager.CheckPasswordSignInAsync(user, decodedPassword, false);

                if (!result.Succeeded)
                    return Result<SuccessTransactionDto>.Fail("Invalid password or Signature mismatch");

            }

            if (!dto.Items.Length.Equals(0))
            {
                var sumUnitPrice = dto.Items.Sum((e) => e.UnitPrice);

                if (dto.TotalAmount != sumUnitPrice)
                {
                    return Result<SuccessTransactionDto>.Fail($"The total valued stated in item details array: {sumUnitPrice} is not equal to value in total ammount: {dto.TotalAmount}");
                }

            }

            var totalDiscount = 0;
            var totalAmount = dto.TotalAmount;

            //discount rules
            totalDiscount = totalAmount switch //in percentage
            {
                < 200 => 0,
                >= 200 and <= 500 => 5,
                >= 501 and <= 800 => 7,
                >= 801 and <= 1200 => 10,
                _ => 15
            };

            //conditional discount
            if (NumberHelper.IsPrime(totalAmount) && totalAmount > 500)
            {
                totalDiscount += 8;
            }

            if (totalAmount % 10 == 5 && totalAmount > 900)
            {
                totalDiscount += 10;
            }

            //Cap discount 
            if (totalDiscount > 20)
            {
                totalDiscount = 20;
            }

            long finalDiscount = (long)(totalDiscount / 100.0 * totalAmount);
            long finalAmount = totalAmount - finalDiscount;

            var successDto = new SuccessTransactionDto
            {
                Result = Enum.TransactionResult.Success,
                TotalAmount = dto.TotalAmount,
                TotalDiscount = finalDiscount,
                FinalAmount = finalAmount,
                ResultMessage = "Transaction processed successfully"
            };

            return Result<SuccessTransactionDto>.Success(successDto);
        }

    }
}