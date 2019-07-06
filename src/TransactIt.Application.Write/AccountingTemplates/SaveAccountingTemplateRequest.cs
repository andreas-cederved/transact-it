using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.AccountingTemplates
{
    public class SaveAccountingTemplateRequest : IRequest
    {
        public SaveAccountingTemplateRequest(int ledgerId, AccountingTemplate accountingTemplate)
        {
            LedgerId = ledgerId;
            AccountingTemplate = accountingTemplate;
        }

        public int LedgerId { get; }
        public AccountingTemplate AccountingTemplate { get; }
    }
}