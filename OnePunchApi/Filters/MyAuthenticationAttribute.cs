using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace OnePunchApi.Filters;

public class MyAuthenticationAttribute : Attribute, IAuthenticationFilter
{
    public bool AllowMultiple { get; }

    public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
    {
        HttpRequestMessage request = context.Request;
        AuthenticationHeaderValue? autorization = request.Headers.Authorization;

        Debug.WriteLine("Inside Authentication Attribute");
        if (autorization is null)
            return;

        if (autorization.Scheme != "My")
            return;

        IPrincipal principal = new ClaimsPrincipal();
        // context.Principal = principal;
        context.ErrorResult = new ErrorResult();
        Debug.WriteLine("Principal Set");
    }

    public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

class ErrorResult : IHttpActionResult
{
    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        return Task.FromResult(response);
    }
}