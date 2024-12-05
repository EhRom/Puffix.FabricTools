# Fabric API Console App

The **Fabric API Console App** is a console application to perform some automatisation tasks, like:

- Listing the worskpaces in a Fabric Tenant

Ressources:

- <https://learn.microsoft.com/en-us/rest/api/fabric/articles/get-started/fabric-api-quickstart>
- <https://learn.microsoft.com/en-us/power-bi/enterprise/service-premium-service-principal>
- <https://github.com/EhRom/Puffix.Rest>
- <https://stackoverflow.com/questions/78226072/generating-token-for-fabric-rest-api-using-client-secret>

Service Principal:

- Redirect Uri : `http://localhost` (Public client/native)
- Permission: PowerBI Service / Application Permission / Tenant.Read.All | Tenant.ReadWrite.All + Grant admin consent
- Secret
- Member of Service Principal security Group

SPN must be part of the workspaces.