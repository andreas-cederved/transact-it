using MediatR;
using System.Collections.Generic;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Read.Ledgers
{
    public class FindAllLedgersRequest : IRequest<List<Ledger>>
    {
    }
}
