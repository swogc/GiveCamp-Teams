Simple application to automatically register guests in an Active Directory domain as guest users.

## Getting started on development

### Create user secrets:
```
dotnet user-secrets set "AzureAd:ClientSecret" "secret from Azure"
dotnet user-secrets set "AzureAd:ClientId" "ClientId from Azure"
dotnet user-secrets set "AzureAd:TenantId" "TenantId from Azure"
```

