using Microsoft.AspNetCore.Authorization;

namespace Software.Api.Vendors;

public class VendorsApi(VendorManager vendorManager) : ControllerBase
{
    [HttpPost("/vendors")]
    [Authorize(Policy = "IsSoftwareManager")]
    public async Task<ActionResult> AddVendorAsync([FromBody] VendorCreateModel request, [FromServices] VendorCreateModelValidator validator)
    {

        var validations = await validator.ValidateAsync(request);

        if (!validations.IsValid)
        {
            return BadRequest();
        }
        var response = await vendorManager.AddVendorAsync(request);
        return Created($"/vendors/{response.Id}", response);
    }

    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorById(Guid id)
    {

        VendorResponseModel? response = await vendorManager.GetVendorByIdAsync(id);


        if (response is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }

}
