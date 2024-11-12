namespace Software.Api.Catalog;

public record CatalogCreateModel
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

public record CatalogItemResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Guid VendorId { get; set; }

    public DateTimeOffset AddedToCatalog { get; set; }
    public string AddedBy { get; set; } = string.Empty;

}