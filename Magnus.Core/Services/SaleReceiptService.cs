using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class SaleReceiptService(
    IReceiptService receiptService,
    IUnitOfWork unitOfWork) : ISaleReceiptService
{
    public void AddSaleReceipt(SaleReceipt saleReceipt)
    {
        throw new NotImplementedException();
    }

    public void UpdateSaleReceipt(SaleReceipt saleReceipt)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteSaleReceiptAsync(SaleReceipt id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task AddRangeAsync(Sale sale, IEnumerable<SaleReceipt> saleReceipts, CancellationToken cancellationToken)
    {
        foreach (var saleReceipt in saleReceipts)
        {
            saleReceipt.SetSaleId(sale.Id);
            saleReceipt.SetClientId(sale.ClientId);
            saleReceipt.SetUserId(sale.UserId);
            if(saleReceipt.ReceiptId == Guid.Empty)
                throw new BusinessRuleException("Informe o id da venda");
        
            var receipt = await receiptService.GetByIdAsync(saleReceipt.ReceiptId, cancellationToken);
            if (receipt is null)
                throw new BusinessRuleException("Informe a forma de recebimento"); 
            
        }
        await unitOfWork.SaleReceipts.AddRangeAsync(saleReceipts, cancellationToken);
    }

    public async Task RevomeFromSaleAsync(Guid saleId, CancellationToken cancellationToken)
    {
        var receipts = await unitOfWork.SaleReceipts.GetAllByExpressionAsync(x => x.SaleId == saleId, cancellationToken);
        unitOfWork.SaleReceipts.RemoveRange(receipts);
    }

    public async Task<IEnumerable<SaleReceipt>> GetSaleReceiptsAsync(Guid saleId, CancellationToken cancellationToken)
    {
        return await unitOfWork.SaleReceipts.GetAllByExpressionAsync(x => x.SaleId == saleId, cancellationToken);
    }
}