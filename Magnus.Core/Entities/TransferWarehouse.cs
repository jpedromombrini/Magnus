namespace Magnus.Core.Entities;

public class TransferWarehouse : EntityBase
{
    public DateTime CreatedAt { get; private set; }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public int WarehouseOriginId { get; private set; }
    public string WarehouseOriginName { get; private set; }
    public int WarehouseDestinyId { get; private set; }
    public string WarehouseDestinyName { get; private set; }
    public List<TransferWarehouseItem> Items { get; private set; }

    private TransferWarehouse(){}
    
    public TransferWarehouse(Guid userId, string userName, int warehouseOriginId, string warehouseOriginName, int warehouseDestinyId, string warehouseDestinyName)
    {
        CreatedAt = DateTime.Now;
        SetUserId(userId);
        SetUserName(userName);
        SetWarehouseOriginId(warehouseOriginId);
        SetWarehouseOriginName(warehouseOriginName);
        SetWarehouseDestinyId(warehouseDestinyId);
        SetWarehouseDestinyName(warehouseDestinyName);
        Items = [];
    }

    public void SetUserId(Guid userId)
    {
        if(userId != Guid.Empty)
            throw new ArgumentException("Informe o Id do usuário");
        UserId = userId;
    }

    public void SetUserName(string userName)
    {
        if(string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Informe o nome do usuário");
        UserName = userName;
    }

    public void SetWarehouseOriginId(int warehouseOriginId)
    {
        if(warehouseOriginId < 0)
            throw new ArgumentException("o código do depósito origem deve ser positivo");
        WarehouseOriginId = warehouseOriginId;
    }

    public void SetWarehouseOriginName(string warehouseOriginName)
    {
        if(string.IsNullOrWhiteSpace(warehouseOriginName))
            throw new ArgumentException("Informe o nome do depósito de origem");
        WarehouseOriginName = warehouseOriginName;
    }

    public void SetWarehouseDestinyId(int warehouseDestinyId)
    {
        if(warehouseDestinyId < 0)
            throw new ArgumentException("o código do depósito destino deve ser positivo");
        WarehouseDestinyId = warehouseDestinyId;
    }

    public void SetWarehouseDestinyName(string warehouseDestinyName)
    {
        if(string.IsNullOrWhiteSpace(warehouseDestinyName))
            throw new ArgumentException("Informe o nome do depósito de destino");
        WarehouseDestinyName = warehouseDestinyName;
    }

    public void AddItem(TransferWarehouseItem item)
    {
        Items.Add(item);   
    }

    public void ResetItems()
    {
        Items.Clear();
    }
    
}