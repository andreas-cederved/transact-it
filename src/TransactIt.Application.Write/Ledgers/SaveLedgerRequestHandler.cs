using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;

namespace TransactIt.Application.Write.Ledgers
{
    public class SaveLedgerRequestHandler : IRequestHandler<SaveLedgerRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveLedgerRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveLedgerRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Ledger>(request.Ledger);
            await _context.Ledgers.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
