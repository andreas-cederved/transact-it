using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Read.Ledgers
{
    public class FindAllLedgersRequestHandler : IRequestHandler<FindAllLedgersRequest, List<Ledger>>
    {
        private readonly NoTrackingContext _context;
        private readonly IMapper _mapper;

        public FindAllLedgersRequestHandler(NoTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Ledger>> Handle(FindAllLedgersRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Ledgers.ToListAsync(cancellationToken);
            return _mapper.Map<List<Ledger>>(result);
        }
    }
}
