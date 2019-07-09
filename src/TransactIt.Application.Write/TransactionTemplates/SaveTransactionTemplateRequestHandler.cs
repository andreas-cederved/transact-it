using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.TransactionTemplates
{
    public class SaveTransactionTemplateRequestHandler : IRequestHandler<SaveTransactionTemplateRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveTransactionTemplateRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveTransactionTemplateRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.TransactionTemplate>(request.TransactionTemplate);

            if (!_context.Ledgers.Any(x => x.Id == request.LedgerId))
            {
                throw new NotFoundException("Ledger", request.LedgerId);
            }

            entity.LedgerId = request.LedgerId;
            await _context.TransactionTemplates.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
