using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Transaction;
using api.Dtos.Transaction.Result;

namespace api.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Result<SuccessTransactionDto>> ProcessTransactionSequence(TransactionDto dto);
    }
}