using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Read.Ledgers
{
    public class FindLedgerByIdRequestHandler : IRequestHandler<FindLedgerByIdRequest, Domain.Models.Ledger>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindLedgerByIdRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Ledger> Handle(FindLedgerByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Ledgers
                .Include(x => x.MainAccountGroups)
                .ThenInclude(x => x.SubAccountGroups)
                .ThenInclude(x => x.Accounts)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            return _mapper.Map<Ledger>(result);
        }
    }
}
