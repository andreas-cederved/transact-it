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

namespace TransactIt.Application.Read.DistributeAmounts
{
    public class DistributeAmountRequestHandler : IRequestHandler<DistributeAmountRequest, List<AccountingEntry>>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public DistributeAmountRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AccountingEntry>> Handle(
            DistributeAmountRequest request,
            CancellationToken cancellationToken)
        {
            var transactionTemplate = await _context.TransactionTemplates
                .Include(x => x.TransactionTemplateRules)
                .FirstOrDefaultAsync(x => x.Id == request.TransactionTemplateId);

            if (transactionTemplate is null)
            {
                throw new NotFoundException("Transaction template", request.TransactionTemplateId);
            }

            var result = transactionTemplate.TransactionTemplateRules
                .Select(x => new Domain.Entities.AccountingEntry
                {
                    Amount = request.Amount * x.Multiplier,
                    AccountId = x.AccountId,
                    Side = x.Side
                })
                .ToList();

            return _mapper.Map<List<AccountingEntry>>(result); ;
        }
    }
}
