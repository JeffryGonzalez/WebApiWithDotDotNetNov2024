using Microsoft.AspNetCore.Authorization;

namespace Software.Api.Catalog;

public class AddingSoftwareController : ControllerBase
{
    [HttpPost("/vendors/{vendorId:guid}/catalog")]
    [Authorize]
    public async Task<ActionResult> CanAddSoftware(
        [FromBody] CatalogCreateModel request,
        [FromRoute] Guid vendorId,
        [FromServices] TimeProvider timeProvider
        )
    {

        // fake response
        var response = new CatalogItemResponseModel
        {
            Name = request.Name,
            Description = request.Description,
            VendorId = vendorId,
            AddedToCatalog = timeProvider.GetLocalNow(),
            AddedBy = User.Identity.Name
        };
        return Ok(response);
    }
}
