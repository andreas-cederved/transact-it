using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Read.Transactions
{
    public class FindTransactionByIdRequestHandler : IRequestHandler<FindTransactionByIdRequest, Domain.Models.TransactionIncludeAccounts>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindTransactionByIdRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionIncludeAccounts> Handle(FindTransactionByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Transactions
                .Include(x => x.AccountingEntries)
                .ThenInclude(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            return _mapper.Map<TransactionIncludeAccounts>(result);
        }
    }
}
