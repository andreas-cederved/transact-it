using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.SubAccountGroups
{
    public class SaveSubAccountGroupRequestHandler : IRequestHandler<SaveSubAccountGroupRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveSubAccountGroupRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveSubAccountGroupRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.SubAccountGroup>(request.SubAccountGroup);

            if (!_context.MainAccountGroups.Any(x =>
                    x.Id == request.MainAccountGroupId))
            {
                throw new NotFoundException("Main account group", request.MainAccountGroupId);
            }

            entity.MainAccountGroupId = request.MainAccountGroupId;
            await _context.SubAccountGroups.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
