using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enum;

namespace api.Dtos.Transaction.Result
{
    public class FailTransactionDto
    {
        public TransactionResult Result { get; set; }
        public string ResultMessage { get; set; } = string.Empty;
    }
}