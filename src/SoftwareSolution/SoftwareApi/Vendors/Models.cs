using FluentValidation;

namespace Software.Api.Vendors;


public record VendorCreateModel
{
    public string Name { get; init; } = string.Empty;

}

public class VendorCreateModelValidator : AbstractValidator<VendorCreateModel>
{
    public VendorCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}

public record VendorResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
}