using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.Accounts
{
    public class SaveAccountRequest : IRequest
    {
        public SaveAccountRequest(int subAccountGroupId, Account account)
        {
            SubAccountGroupId = subAccountGroupId;
            Account = account;
        }

        public int SubAccountGroupId { get; }
        public Account Account { get; }
    }
}