namespace HtTemplate.Catalog;

public class AddingSoftwareController : ControllerBase
{
    [HttpPost("/vendors/7473ee24-54d2-48f4-8e84-d240d65e4b16/catalog")]
    public async Task<ActionResult> CanAddSoftware(
        [FromBody] CatalogCreateModel request
        )
    {

        // fake response
        var response = new CatalogItemResponseModel
        {
            Name = request.Name,
            Description = request.Description,
        };
        return Ok(response);
    }
}
