namespace Magnus.Core.Entities;

public class CostCenterSubGroup : EntityBase
{
    public string Code { get; set; } 
    public string Name { get; set; } 
    public Guid? CostCenterGroupId { get; set; }
    public CostCenterGroup? CostCenterGroup { get; set; }
    public List<CostCenter>? CostCenters { get; set; }
    
    public CostCenterSubGroup(string code, string name)
    {
        SetCode(code);
        SetName(name);
        CostCenters = [];
    }
    public void SetCode(string code)
    {
        if(string.IsNullOrEmpty(code))
            throw new ArgumentNullException("O código não pode ser nulo.");
        if(code.Length != 5)
            throw new ArgumentNullException("O código deve ter 5 caracteres.");
        Code = code;
    }

    public void SetName(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O Nome não pode ser nulo.");
        Name = name;
    }
    public void AddCostCenter(CostCenter costCenter)
    {
        if (costCenter == null)
            throw new ArgumentException("Informe o Centro de custo");
        CostCenters?.Add(costCenter);
    }
    public void SetCostCenterGroup(CostCenterGroup costCenterGroup)
    {
        if (costCenterGroup == null)
            throw new ArgumentException("Informe o SubGrupo");
        
        if (costCenterGroup.Id == Guid.Empty)
            throw new ArgumentException("Informe o Id do SubGrupo");

        CostCenterGroup = costCenterGroup;
        CostCenterGroupId = costCenterGroup.Id;
    }
}