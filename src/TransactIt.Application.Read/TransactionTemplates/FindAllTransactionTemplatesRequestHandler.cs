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

namespace TransactIt.Application.Read.TransactionTemplates
{
    public class FindAllTransactionTemplatesRequestHandler : IRequestHandler<FindAllTransactionTemplatesRequest, List<TransactionTemplate>>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindAllTransactionTemplatesRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TransactionTemplate>> Handle(FindAllTransactionTemplatesRequest request, CancellationToken cancellationToken)
        {
            var ledger = await _context.Ledgers
                .Include(x => x.TransactionTemplates)
                .ThenInclude(x => x.TransactionTemplateRules)
                .FirstOrDefaultAsync(x => x.Id == request.LedgerId);

            if (ledger is null)
            {
                throw new NotFoundException("TransactionTemplates", request.LedgerId);
            }

            return _mapper.Map<List<TransactionTemplate>>(ledger.TransactionTemplates);
        }
    }
}
