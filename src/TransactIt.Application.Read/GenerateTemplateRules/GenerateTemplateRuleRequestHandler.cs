using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Domain.Models;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Read.GenerateTemplateRules
{
    public class GenerateTemplateRuleRequestHandler : IRequestHandler<GenerateTemplateRuleRequest, List<TransactionTemplateRule>>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public GenerateTemplateRuleRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TransactionTemplateRule>> Handle(
            GenerateTemplateRuleRequest request,
            CancellationToken cancellationToken)
        {
            var transaction = await _context.Transactions
                .Include(x => x.AccountingEntries)
                .FirstOrDefaultAsync(x => x.Id == request.TransactionId);

            if (transaction is null)
            {
                throw new NotFoundException("Transaction", request.TransactionId);
            }

            var largestAmount = transaction.AccountingEntries.Max(x => x.Amount);

            var result = transaction.AccountingEntries
                .Select(x => new Domain.Entities.TransactionTemplateRule
                {
                    //Amount = request.Amount * x.Multiplier,
                    AccountId = x.AccountId,
                    Side = x.Side,
                    Multiplier = x.Amount / largestAmount
                })
                .ToList();

            return _mapper.Map<List<TransactionTemplateRule>>(result); ;
        }
    }
}
