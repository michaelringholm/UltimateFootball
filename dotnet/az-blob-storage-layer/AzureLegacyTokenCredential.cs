using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

public class AzureLegacyTokenCredential : Azure.Core.TokenCredential
{
    private Microsoft.Azure.Storage.Auth.TokenCredential storageTokenCredential;

    public AzureLegacyTokenCredential(Microsoft.Azure.Storage.Auth.TokenCredential storageTokenCredential) {
        this.storageTokenCredential = storageTokenCredential;
    }
    
    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        var token = storageTokenCredential.Token;
        var accessToken = new AccessToken(token, DateTimeOffset.Now.AddHours(1));
        return accessToken;
    }

    public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
    {
        var token = storageTokenCredential.Token;
        var accessToken = new AccessToken(token, DateTimeOffset.Now.AddHours(1));
        return accessToken;
    }
}