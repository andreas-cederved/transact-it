using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.SubAccountGroups
{
    public class SaveSubAccountGroupRequest : IRequest
    {
        public SaveSubAccountGroupRequest(
            int mainAccountGroupId,
            SubAccountGroup subAccountGroup)
        {
            MainAccountGroupId = mainAccountGroupId;
            SubAccountGroup = subAccountGroup;
        }

        public int MainAccountGroupId { get; }
        public SubAccountGroup SubAccountGroup { get; }
    }
}
