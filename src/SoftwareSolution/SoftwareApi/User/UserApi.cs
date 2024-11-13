using Marten;
using Software.Api.Catalog;

namespace Software.Api.User;

//public class UserApi(IDocumentSession session) : ControllerBase
//{
//    [HttpGet("/techs")]
//    public async Task<ActionResult> GetAllTechs()
//    {
//        var techs = await session.Query<UserInformation>().ToListAsync();
//        return Ok(new { data = techs });
//    }

//    [HttpGet("/techs/{id:guid}/software")]
//    public async Task<ActionResult> GetSoftwareForTech(Guid id)
//    {
//        // 404?

//        // TODO: HAve the user feature, create an interface that the CatalogManager implements.
//        var response = await session.Query<CatalogItemEntity>()
//            .Where(c => c.AddedBySub == id.ToString())
//            .ToListAsync();

//        return Ok(new { data = response });
//    }
//}


public static class TechApi
{
    public static WebApplication MapTechApi(this WebApplication app)
    {
        app.MapGet("/techs", GetAllTechs);
        app.MapGet("/techs/{id:guid}/software", GetTechById);
        return app;
    }

    public static async Task<Microsoft.AspNetCore.Http.HttpResults.Ok<IReadOnlyList<UserInformationResponseModel>>> GetAllTechs([FromServices] IDocumentSession session)
    {
        var techs = await session.Query<UserInformation>()
            .Select(u => new UserInformationResponseModel
            {
                Id = u.Id,
                Sub = u.Sub
            })
            .ToListAsync();
        return TypedResults.Ok(techs);
    }

    public static async Task<Microsoft.AspNetCore.Http.HttpResults.Ok<IReadOnlyList<CatalogItemEntity>>> GetTechById([FromServices] IDocumentSession session, Guid id)
    {
        //        // 404?

        // TODO: HAve the user feature, create an interface that the CatalogManager implements.
        var response = await session.Query<CatalogItemEntity>()
            .Where(c => c.AddedBySub == id.ToString())
            .ToListAsync();

        return TypedResults.Ok(response);
    }
}

public record UserInformationResponseModel
{
    public Guid Id { get; set; }
    public string Sub { get; set; } = string.Empty;

}