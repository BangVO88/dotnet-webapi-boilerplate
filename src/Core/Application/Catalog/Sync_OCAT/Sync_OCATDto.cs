namespace FSH.WebApi.Application.Catalog.Sync_OCAT;

public class Sync_OCATDto : IDto
{
    public Guid Id { get; set; }
    public string CatCode { get; set; } = default!;
    public string CatName { get; set; } = default!;
    public int CatOrder { get; set; }
}