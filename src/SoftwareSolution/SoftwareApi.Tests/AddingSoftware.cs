
using Alba;
using Alba.Security;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using Software.Api.Catalog;
using System.Security.Claims;

namespace SoftwareApi.Tests;
public class AddingSoftware
{
    [Fact]
    public async Task CanAddAnItemToTheCatalog()
    {
        // given
        // start up an instance of the api 
        var fakeTime = new DateTimeOffset(1969, 4, 20, 23, 59, 59, TimeSpan.FromHours(-5));
        var fakeTimeProvider = new FakeTimeProvider(fakeTime);

        var vendorId = Guid.Parse("7473ee24-54d2-48f4-8e84-d240d65e4b16");
        var requestBody = new CatalogCreateModel
        {
            Name = "Visual Studio Code",
            Description = "Editor for Programmers"
        };

        var fakeIdentity = new AuthenticationStub()
            .WithName("bob-smith")
            .With(new System.Security.Claims.Claim(ClaimTypes.Role, "software-center"));
        // when
        var host = await AlbaHost.For<Program>(cfg =>
        {
            cfg.ConfigureTestServices(services =>
            {
                var fakeVendorLookup = Substitute.For<ILookupVendors>();
                fakeVendorLookup.VendorExistsAsync(vendorId).Returns(true);
                services.AddScoped<ILookupVendors>(sp => fakeVendorLookup);
            });

        }, fakeIdentity);
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
