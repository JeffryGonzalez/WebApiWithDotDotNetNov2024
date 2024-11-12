using Marten;

namespace Software.Api.Vendors;

public class VendorManager(IDocumentSession session)
{
    public async Task<VendorResponseModel> AddVendorAsync(VendorCreateModel request)
    {
        var entity = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        session.Store(entity);
        await session.SaveChangesAsync();
        var response = new VendorResponseModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
        return response;
    }

    public async Task<VendorResponseModel?> GetVendorByIdAsync(Guid id)
    {
        return await session.Query<VendorEntity>()
            .Where(v => v.Id == id)
            .Select(v => new VendorResponseModel
            {
                Id = v.Id,
                Name = v.Name,
            })

            .SingleOrDefaultAsync();
    }
}