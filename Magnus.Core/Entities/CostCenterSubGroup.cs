namespace Magnus.Core.Entities;

public class CostCenterSubGroup : EntityBase
{
    public CostCenterSubGroup(string code, string name, Guid costCenterGroupId)
    {
        SetCode(code);
        SetName(name);
        SetCostCenterGroupId(costCenterGroupId);
        CostCenters = [];
    }

    public string Code { get; private set; }
    public string Name { get; private set; }
    public Guid CostCenterGroupId { get; private set; }
    public CostCenterGroup CostCenterGroup { get; private set; }
    public ICollection<CostCenter> CostCenters { get; }

    public void SetCode(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new ArgumentNullException("O código não pode ser nulo.");
        if (code.Length != 5)
            throw new ArgumentNullException("O código deve ter 5 caracteres.");
        Code = code;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O Nome não pode ser nulo.");
        Name = name.ToUpper();
    }

    public void AddCostCenter(CostCenter costCenter)
    {
        if (costCenter == null)
            throw new ArgumentException("Informe o Centro de custo");
        CostCenters?.Add(costCenter);
    }

    public void SetCostCenterGroup(CostCenterGroup costCenterGroup)
    {
        CostCenterGroup = costCenterGroup;
    }

    public void SetCostCenterGroupId(Guid costCenterGroupId)
    {
        if (costCenterGroupId == Guid.Empty)
            throw new ArgumentException("Informe o Id do SubGrupo");
        CostCenterGroupId = costCenterGroupId;
    }
}