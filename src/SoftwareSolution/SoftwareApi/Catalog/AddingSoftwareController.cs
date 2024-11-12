using Microsoft.AspNetCore.Authorization;

namespace Software.Api.Catalog;

public class AddingSoftwareController : ControllerBase
{
    [HttpPost("/vendors/{vendorId:guid}/catalog")]
    [Authorize(Policy = "IsSoftwareCenter")]
    public async Task<ActionResult> CanAddSoftware(
        [FromBody] CatalogCreateModel request,
        [FromRoute] Guid vendorId,
        [FromServices] TimeProvider timeProvider
        )
    {

        // validate the references - User identity (Authorize)  - Vendor Id.
        // validate the body (the owned data) // FluentValidation, DataAttributes, or just write some code.
        // if those are bad, send appropriate http response messages

        // uh, actually do something with this? do the "unsafe" thing. (add it to the database)


        // fake response
        var response = new CatalogItemResponseModel
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,

        };
        return Ok(response); // 201 Created - with a Location header.
    }
}
