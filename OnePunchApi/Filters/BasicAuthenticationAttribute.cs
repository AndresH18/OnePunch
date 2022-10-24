using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Filters;

namespace OnePunchApi.Filters;

// Look here https://github.com/aspnet/samples/blob/main/samples/aspnet/WebApi/BasicAuthentication/BasicAuthentication/Filters/BasicAuthenticationAttribute.cs
public abstract class BasicAuthenticationAttribute : Attribute, IAuthenticationFilter
{
    public string Realm { get; set; }

    public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
    {
        HttpRequestMessage request = context.Request;
        AuthenticationHeaderValue? autorization = request.Headers.Authorization;

        if (autorization is null)
        {
            // No authentication was attempted (for this authentication method).
            // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
            return;
        }

        if (autorization.Scheme != "Basic")
        {
            // No authentication was attempted (for this authentication method).
            // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
            return;
            /*
             I dont know why, but I think that its because it is the 'basic'. So it's considered the same as 
             if no authentication was attempted
             */
        }

        if (string.IsNullOrWhiteSpace(autorization.Parameter))
        {
            // Authentication was attempted but failed. Set ErrorResult to indicate an error.
            //// context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
            return;
        }

        Tuple<string, string>? userNameAndPassword = ExtractUserNameAndPassword(autorization.Parameter);

        if (userNameAndPassword == null)
        {
            // Authentication was attempted but failed. Set ErrorResult to indicate an error.
            //// context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
            return;
        }

        string userName = userNameAndPassword.Item1;
        string password = userNameAndPassword.Item2;

        IPrincipal? principal = await AuthenticateAsync(userName, password, cancellationToken);

        if (principal == null)
        {
            // Authentication was attempted but failed. Set ErrorResult to indicate an error.
            //// context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
        }
        else
        {
            // Authentication was attempted and succeeded. Set Principal to the authenticated user.
            context.Principal = principal;
        }
    }

    protected abstract Task<IPrincipal?> AuthenticateAsync(string userName, string password,
        CancellationToken cancellationToken);

    private static Tuple<string, string>? ExtractUserNameAndPassword(string authorizationParameter)
    {
        byte[] credentialBytes;

        try
        {
            credentialBytes = Convert.FromBase64String(authorizationParameter);
        }
        catch (FormatException)
        {
            return null;
        }

        // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
        // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
        // used in practice and defines behavior only for ASCII.
        Encoding encoding = Encoding.ASCII;
        // Make a writable copy of the encoding to enable setting a decoder fallback.
        encoding = (Encoding) encoding.Clone();
        // Fail on invalid bytes rather than silently replacing and continuing.
        encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
        string decodedCredentials;

        try
        {
            decodedCredentials = encoding.GetString(credentialBytes);
        }
        catch (DecoderFallbackException)
        {
            return null;
        }

        if (String.IsNullOrEmpty(decodedCredentials))
        {
            return null;
        }

        int colonIndex = decodedCredentials.IndexOf(':');

        if (colonIndex == -1)
        {
            return null;
        }

        string userName = decodedCredentials.Substring(0, colonIndex);
        string password = decodedCredentials.Substring(colonIndex + 1);
        return new Tuple<string, string>(userName, password);
    }

    public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
    {
        Challenge(context);
        return Task.FromResult(0);
    }

    private void Challenge(HttpAuthenticationChallengeContext context)
    {
        string parameter;

        if (String.IsNullOrEmpty(Realm))
        {
            parameter = null;
        }
        else
        {
            // A correct implementation should verify that Realm does not contain a quote character unless properly
            // escaped (precededed by a backslash that is not itself escaped).
            parameter = "realm=\"" + Realm + "\"";
        }

        // context.ChallengeWith("Basic", parameter);
    }


    public bool AllowMultiple { get; }
}