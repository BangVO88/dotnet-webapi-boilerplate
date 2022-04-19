namespace FSH.WebApi.Application.Catalog.Sync_OCAT;

public class GetSync_OCATRequest : IRequest<Sync_OCATDto>
{
    public Guid Id { get; set; }

    public GetSync_OCATRequest(Guid id) => Id = id;
}

public class Synn_OCATByIdSpec : Specification<Sync_OCAT_T, Sync_OCATDto>, ISingleResultSpecification
{
    public Synn_OCATByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetSync_OCATRequestHandler : IRequestHandler<GetSync_OCATRequest, Sync_OCATDto>
{
    private readonly IRepository<Sync_OCAT_T> _repository;
    private readonly IStringLocalizer _t;

    public GetSync_OCATRequestHandler(IRepository<Sync_OCAT_T> repository, IStringLocalizer<GetSync_OCATRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<Sync_OCATDto> Handle(GetSync_OCATRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Sync_OCAT_T, Sync_OCATDto>)new Synn_OCATByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Sync_OCAT_T {0} Not Found.", request.Id]);
}