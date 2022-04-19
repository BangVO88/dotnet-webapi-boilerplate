using FSH.WebApi.Application.Catalog.Sync_OCAT;
namespace FSH.WebApi.Host.Controllers.Catalog;

public class Sync_OCATsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [MustHavePermission(FSHAction.Search, FSHResource.Sync_OCAT)]
    [OpenApiOperation("Search Sync_OCAT using available filters.", "")]
    public Task<PaginationResponse<Sync_OCATDto>> SearchAsync(SearchSync_OCATsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Sync_OCAT)]
    [OpenApiOperation("Get brand details.", "")]
    public Task<Sync_OCATDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetSync_OCATRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Sync_OCAT)]
    [OpenApiOperation("Create a new Sync_OCAT.", "")]
    public Task<Guid> CreateAsync(CreateSync_OCATRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Sync_OCAT)]
    [OpenApiOperation("Update a Sync_OCAT.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateSync_OCATRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Sync_OCAT)]
    [OpenApiOperation("Delete a Sync_OCAT.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteSync_OCATRequest(id));
    }
}