namespace Software.Api.Vendors;

public class VendorsApi
{
}


public record VendorCreateModel
{
    public string Name { get; init; } = string.Empty;

}

public record VendorResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
}