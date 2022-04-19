namespace FSH.WebApi.Application.Catalog.Sync_OCAT;

public class Sync_OCATByNameSpec : Specification<Sync_OCAT_T>, ISingleResultSpecification
{
    public Sync_OCATByNameSpec(string name) =>
        Query.Where(b => b.CatName == name);
}