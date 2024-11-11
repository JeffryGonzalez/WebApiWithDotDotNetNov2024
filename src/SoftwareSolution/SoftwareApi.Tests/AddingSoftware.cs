
using Alba;
using HtTemplate.Catalog;

namespace SoftwareApi.Tests;
public class AddingSoftware
{
    [Fact]
    public async Task CanAddAnItemToTheCatalog()
    {
        // given
        // start up an instance of the api 
        var requestBody = new CatalogCreateModel
        {
            Name = "Visual Studio Code",
            Description = "Editor for Programmers"
        };

        // when
        var host = await AlbaHost.For<Program>();
        // do something to it.
        var responseFromPost = await host.Scenario(api =>
        {
            api.Post
            .Json(requestBody)
            .ToUrl("/vendors/7473ee24-54d2-48f4-8e84-d240d65e4b16/catalog");

            api.StatusCodeShouldBeOk();
        });
        // I want to post this data to this url 
        // and I should get back this status code
        // and I should be able to look that up again..
        // verify it.

        Assert.NotNull(responseFromPost);
        var postResponseModel = await responseFromPost.ReadAsJsonAsync<CatalogItemResponseModel>();

        Assert.Equal(requestBody.Name, postResponseModel.Name);
        Assert.Equal(requestBody.Description, postResponseModel.Description);
    }
}
