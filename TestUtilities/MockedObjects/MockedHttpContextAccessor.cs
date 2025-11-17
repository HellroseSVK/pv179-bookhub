using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TestUtilities.MockedObjects;

public static class MockHttpContextAccessor
{
    public static IHttpContextAccessor Create(string? userId = "test-user-id")
    {
        var context = new DefaultHttpContext();

        if (userId == null)
        {
            return new HttpContextAccessor { HttpContext = context };
        }

        var identity = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, userId)
        ], "TestAuth");

        context.User = new ClaimsPrincipal(identity);

        return new HttpContextAccessor { HttpContext = context };
    }
}