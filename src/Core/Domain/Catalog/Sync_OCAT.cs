namespace FSH.WebApi.Domain.Catalog;

public class Sync_OCAT : AuditableEntity, IAggregateRoot
{
    public string? CatCode { get; private set; }
    public string? CatName { get; private set; }
    public int? CatOrder { get; private set; }

    public Sync_OCAT(string? catCode, string? catName, int? catOrder)
    {
        CatCode = catCode;
        CatName = catName;
        CatOrder = catOrder;
    }

    public Sync_OCAT Update(string? catCode, string? catName, int? catOrder)
    {
        if (catCode is not null && CatCode?.Equals(catCode) is not true) CatCode = catCode;
        if (catName is not null && CatName?.Equals(catName) is not true) CatName = catName;
        if (catOrder is not null && CatOrder?.Equals(catOrder) is not true) CatOrder = catOrder;
        return this;
    }
}