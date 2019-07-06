﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;

namespace TransactIt.Application.Write.FinancialTransactions
{
    public class SaveFinancialTransactionRequestHandler : IRequestHandler<SaveFinancialTransactionRequest>
    {
        private readonly TrackingContext _context;
        private readonly IMapper _mapper;

        public SaveFinancialTransactionRequestHandler(TrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveFinancialTransactionRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.FinancialTransaction>(request.FinancialTransaction);

            if (!_context.Ledgers.Any(x => x.Id == request.LedgerId))
            {
                throw new NotFoundException("Ledger", request.LedgerId);
            }

            var lastIdentifyingCodeUsed = await _context.FinancialTransactions
                .Where(x => x.LedgerId == request.LedgerId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => x.IdentifyingCode)
                .FirstOrDefaultAsync();

            entity.IdentifyingCode = lastIdentifyingCodeUsed + 1;
            entity.LedgerId = request.LedgerId;
            await _context.FinancialTransactions.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
