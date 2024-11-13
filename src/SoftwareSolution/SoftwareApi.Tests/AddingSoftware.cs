
using Alba;
using Alba.Security;
using Microsoft.Extensions.Time.Testing;
using Software.Api.Catalog;
using System.Security.Claims;

namespace SoftwareApi.Tests;
[Trait("Category", "SystemTest")]
public class AddingSoftware
{
    [Fact]
    public async Task CanAddAnItemToTheCatalog()
    {
        // given
        // start up an instance of the api 
        var fakeTime = new DateTimeOffset(1969, 4, 20, 23, 59, 59, TimeSpan.FromHours(-5));
        var fakeTimeProvider = new FakeTimeProvider(fakeTime);

        var vendorId = Guid.Parse("d18a9612-d16d-44d0-8654-de4bb33730c7"); // Id for Test Data Vendor of Microsoft
        var requestBody = new CatalogCreateModel
        {
            Name = "Visual Studio Code",
            Description = "Editor for Programmers"
        };

        var fakeIdentity = new AuthenticationStub()
            .WithName("bob-smith")
            .With(new System.Security.Claims.Claim(ClaimTypes.Role, "software-center"));
        // when
        var host = await AlbaHost.For<Program>(fakeIdentity);
        // Post a new piece of software to the catalog
        var responseFromPost = await host.Scenario(api =>
        {
            api.Post
            .Json(requestBody)
            .ToUrl($"/vendors/{vendorId}/catalog");

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

        // Get that newly created piece of software from the API
        var responseFromGet = await host.Scenario(api =>
        {
            api.Get.Url($"/vendors/{vendorId}/catalog/{postResponseModel.Id}");
            api.StatusCodeShouldBeOk();

        });

        Assert.NotNull(responseFromGet);

        var getResponseModel = await responseFromGet.ReadAsJsonAsync<CatalogItemResponseModel>();

        Assert.Equal(getResponseModel, postResponseModel);

    }
}
