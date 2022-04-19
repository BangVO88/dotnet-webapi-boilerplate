namespace FSH.WebApi.Application.Catalog.Sync_OCAT;

public class SearchSync_OCATsRequest : PaginationFilter, IRequest<PaginationResponse<Sync_OCATDto>>
{
}

public class Sync_OCATsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Sync_OCAT_T, Sync_OCATDto>
{
    public Sync_OCATsBySearchRequestSpec(SearchSync_OCATsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.CatCode, !request.HasOrderBy());
}

public class SearchSyncOCATsRequestHandler : IRequestHandler<SearchSync_OCATsRequest, PaginationResponse<Sync_OCATDto>>
{
    private readonly IReadRepository<Sync_OCAT_T> _repository;

    public SearchSyncOCATsRequestHandler(IReadRepository<Sync_OCAT_T> repository) => _repository = repository;

    public async Task<PaginationResponse<Sync_OCATDto>> Handle(SearchSync_OCATsRequest request, CancellationToken cancellationToken)
    {
        var spec = new Sync_OCATsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}