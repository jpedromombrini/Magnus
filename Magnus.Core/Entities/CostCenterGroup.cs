using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class CostCenterGroup : EntityBase
{
    public CostCenterGroup(string code, string name, CostcenterGroupType costcenterGroupType)
    {
        SetCode(code);
        SetName(name);
        SetCostCenterGroupType(costcenterGroupType);
        CostCenterSubGroups = [];
    }

    public string Code { get; set; }
    public string Name { get; set; }
    public CostcenterGroupType CostcenterGroupType { get; set; }
    public ICollection<CostCenterSubGroup> CostCenterSubGroups { get; set; }

    public void SetCode(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new ArgumentNullException("O código não pode ser nulo.");
        if (code.Length != 2)
            throw new ArgumentNullException("O código deve ter 2 caracteres.");
        Code = code;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O Nome não pode ser nulo.");
        Name = name.ToUpper();
    }

    public void AddCostCenterSubGroup(CostCenterSubGroup costCenterSubGroup)
    {
        if (costCenterSubGroup == null)
            throw new ArgumentException("Informe o SubGrupo");
        CostCenterSubGroups?.Add(costCenterSubGroup);
    }

    public void SetCostCenterGroupType(CostcenterGroupType costcenterGroupType)
    {
        CostcenterGroupType = costcenterGroupType;
    }
}