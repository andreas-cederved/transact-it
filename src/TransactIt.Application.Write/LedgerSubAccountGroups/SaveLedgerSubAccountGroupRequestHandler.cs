using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.LedgerSubAccountGroups
{
    public class SaveLedgerSubAccountGroupRequestHandler : IRequestHandler<SaveLedgerSubAccountGroupRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveLedgerSubAccountGroupRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveLedgerSubAccountGroupRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.LedgerSubAccountGroup>(request.LedgerSubAccountGroup);

            if (!_context.LedgerMainAccountGroups.Any(x =>
                    x.Id == request.LedgerMainAccountGroupId
                    && x.LedgerId == request.LedgerId))
            {
                throw new NotFoundException("Ledger main account group", request.LedgerMainAccountGroupId);
            }

            entity.LedgerMainAccountGroupId = request.LedgerMainAccountGroupId;
            await _context.LedgerSubAccountGroups.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
