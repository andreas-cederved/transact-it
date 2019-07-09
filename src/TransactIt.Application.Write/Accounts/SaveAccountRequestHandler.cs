using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.Accounts
{
    public class SaveAccountRequestHandler : IRequestHandler<SaveAccountRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveAccountRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveAccountRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Account>(request.Account);

            if (!_context.SubAccountGroups.Any(x => x.Id == request.SubAccountGroupId))
            {
                throw new NotFoundException("S account group", request.SubAccountGroupId);
            }

            entity.SubAccountGroupId = request.SubAccountGroupId;
            await _context.Accounts.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
