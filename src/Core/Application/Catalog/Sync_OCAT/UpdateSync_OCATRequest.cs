namespace FSH.WebApi.Application.Catalog.Sync_OCAT;

public class UpdateSync_OCATRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? CatCode { get; set; } = default!;
    public string? CatName { get; set; } = default!;
    public int? CatOrder { get; set; }
}

public class UpdateSync_OCATRequestValidator : CustomValidator<UpdateSync_OCATRequest>
{
    public UpdateSync_OCATRequestValidator(IRepository<Sync_OCAT_T> repository, IStringLocalizer<UpdateSync_OCATRequestValidator> T) =>
        RuleFor(p => p.CatName)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (ocat, name, ct) =>
                    await repository.GetBySpecAsync(new Sync_OCATByNameSpec(name), ct)
                        is not Sync_OCAT_T existingBrand || existingBrand.Id == ocat.Id)
                .WithMessage((_, name) => T["Sync_OCAT_T {0} already Exists.", name]);
}

public class UpdateSync_OCATRequestHandler : IRequestHandler<UpdateSync_OCATRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Sync_OCAT_T> _repository;
    private readonly IStringLocalizer _t;

    public UpdateSync_OCATRequestHandler(IRepositoryWithEvents<Sync_OCAT_T> repository, IStringLocalizer<UpdateSync_OCATRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateSync_OCATRequest request, CancellationToken cancellationToken)
    {
        var ocat = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ocat
        ?? throw new NotFoundException(_t["Sync_OCAT_T {0} Not Found.", request.Id]);

        ocat.Update(request.CatCode, request.CatName, request.CatOrder);

        await _repository.UpdateAsync(ocat, cancellationToken);

        return request.Id;
    }
}