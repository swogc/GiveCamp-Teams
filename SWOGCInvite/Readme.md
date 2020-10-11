# Need to create user secrets
dotnet user-secrets set "AzureAd:ClientSecret" "secret from Azure"
dotnet user-secrets set "AzureAd:ClientId" "ClientId from Azure"
dotnet user-secrets set "AzureAd:TenantId" "TenantId from Azure"