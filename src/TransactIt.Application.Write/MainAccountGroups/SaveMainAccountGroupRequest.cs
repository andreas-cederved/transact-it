using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.MainAccountGroups
{
    public class SaveMainAccountGroupRequest : IRequest
    {
        public SaveMainAccountGroupRequest(int ledgerId, MainAccountGroup mainAccountGroup)
        {
            LedgerId = ledgerId;
            MainAccountGroup = mainAccountGroup;
        }

        public int LedgerId { get; }
        public MainAccountGroup MainAccountGroup { get; }
    }
}
