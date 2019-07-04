using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.LedgerMainAccountGroups
{
    public class SaveLedgerMainAccountGroupRequestHandler : IRequestHandler<SaveLedgerMainAccountGroupRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveLedgerMainAccountGroupRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveLedgerMainAccountGroupRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.LedgerMainAccountGroup>(request.LedgerMainAccountGroup);

            if (!_context.Ledgers.Any(x => x.Id == request.LedgerId))
            {
                throw new NotFoundException("Ledger", request.LedgerId);
            }

            entity.LedgerId = request.LedgerId;
            await _context.LedgerMainAccountGroups.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
