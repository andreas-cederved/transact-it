using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.AccountingTemplates
{
    public class SaveAccountingTemplateRequestHandler : IRequestHandler<SaveAccountingTemplateRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveAccountingTemplateRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveAccountingTemplateRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.AccountingTemplate>(request.AccountingTemplate);

            if (!_context.Ledgers.Any(x => x.Id == request.LedgerId))
            {
                throw new NotFoundException("Ledger", request.LedgerId);
            }

            entity.LedgerId = request.LedgerId;
            await _context.AccountingTemplates.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
