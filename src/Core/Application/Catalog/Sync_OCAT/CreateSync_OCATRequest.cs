namespace FSH.WebApi.Application.Catalog.Sync_OCAT;

public class CreateSync_OCATRequest : IRequest<Guid>
{
    public string CatCode { get; set; } = default!;
    public string CatName { get; set; } = default!;
    public int CatOrder { get; set; }
}

public class CreateSync_OCATRequestValidator : CustomValidator<CreateSync_OCATRequest>
{
    public CreateSync_OCATRequestValidator(IReadRepository<Sync_OCAT_T> repository, IStringLocalizer<CreateSync_OCATRequestValidator> T) =>
        RuleFor(p => p.CatName)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new Sync_OCATByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Sync_OCAT {0} already Exists.", name]);
}

public class CreateSync_OCATRequestHandler : IRequestHandler<CreateSync_OCATRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Sync_OCAT_T> _repository;

    public CreateSync_OCATRequestHandler(IRepositoryWithEvents<Sync_OCAT_T> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateSync_OCATRequest request, CancellationToken cancellationToken)
    {
        var sync_ocat = new Sync_OCAT_T(request.CatCode, request.CatName, request.CatOrder);

        await _repository.AddAsync(sync_ocat, cancellationToken);

        return sync_ocat.Id;
    }
}