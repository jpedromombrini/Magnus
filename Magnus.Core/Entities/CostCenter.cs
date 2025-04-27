namespace Magnus.Core.Entities;

public class CostCenter : EntityBase
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public Guid CostCenterSubGroupId { get; private set; }
    public CostCenterSubGroup CostCenterSubGroup { get; set; }
    private CostCenter(){}
    public CostCenter(string code, string name, Guid costCenterSubGroupId)
    {
        SetCode(code);
        SetName(name);
        SetCostCenterSubGroupId(costCenterSubGroupId);
    }

    public void SetCode(string code)
    {
        if(string.IsNullOrEmpty(code))
            throw new ArgumentNullException("O código não pode ser nulo.");
        if(code.Length != 8)
            throw new ArgumentNullException("O código deve ter 8 caracteres.");
        Code = code;
    }

    public void SetName(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O Nome não pode ser nulo.");
        Name = name;
    }

    public void SetCostCenterSubGroupId(Guid costCenterSubGroupId)
    {   
        if (costCenterSubGroupId == Guid.Empty)
            throw new ArgumentException("Informe o Id do SubGrupo");
        
        CostCenterSubGroupId = costCenterSubGroupId;
    }
}