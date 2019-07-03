using MediatR;

namespace TransactIt.Application.Read.Ledgers
{
    public class FindLedgerByIdRequest : IRequest<Domain.Models.Ledger>
    {
        public FindLedgerByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
