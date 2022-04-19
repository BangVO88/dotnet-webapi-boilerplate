namespace FSH.WebApi.Application.Catalog.Sync_OCAT;

public class DeleteSync_OCATRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteSync_OCATRequest(Guid id) => Id = id;
}

public class DeleteSync_OCATRequestHandler : IRequestHandler<DeleteSync_OCATRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Sync_OCAT_T> _ocatRepo;
    private readonly IStringLocalizer _t;

    public DeleteSync_OCATRequestHandler(IRepositoryWithEvents<Sync_OCAT_T> Sync_OCATRepo, IStringLocalizer<DeleteSync_OCATRequestHandler> localizer) =>
        (_ocatRepo, _t) = (Sync_OCATRepo, localizer);

    public async Task<Guid> Handle(DeleteSync_OCATRequest request, CancellationToken cancellationToken)
    {
        var sync_ocat = await _ocatRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = sync_ocat ?? throw new NotFoundException(_t["Sync_OCAT {0} Not Found."]);

        await _ocatRepo.DeleteAsync(sync_ocat, cancellationToken);

        return request.Id;
    }
}