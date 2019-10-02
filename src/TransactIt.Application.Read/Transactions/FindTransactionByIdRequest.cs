using MediatR;

namespace TransactIt.Application.Read.Transactions
{
    public class FindTransactionByIdRequest : IRequest<Domain.Models.TransactionIncludeAccounts>
    {
        public FindTransactionByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
