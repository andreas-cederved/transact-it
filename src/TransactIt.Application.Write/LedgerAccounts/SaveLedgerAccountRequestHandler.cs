using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.LedgerAccounts
{
    public class SaveLedgerAccountRequestHandler : IRequestHandler<SaveLedgerAccountRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveLedgerAccountRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveLedgerAccountRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.LedgerAccount>(request.LedgerAccount);

            if (!_context.LedgerSubAccountGroups.Any(x => x.Id == request.LedgerSubAccountGroupId))
            {
                throw new NotFoundException("Ledger sub account group", request.LedgerSubAccountGroupId);
            }

            entity.LedgerSubAccountGroupId = request.LedgerSubAccountGroupId;
            await _context.LedgerAccounts.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
