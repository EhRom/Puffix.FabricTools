# Fabric Tools Console App

The **Fabric Tools Console App** is a console application to perform some automatisation tasks, based on the [Fabric REST API](https://learn.microsoft.com/en-us/rest/api/fabric/articles/using-fabric-apis) funcitonnalities.

The following functionnalities are available:
- Authentication:

	- Authenticate,
	- Save, load and erase token to not reauthenticate at each execution.

- Inventory:
	
	- [List capacities](https://learn.microsoft.com/en-us/rest/api/fabric/core/capacities/list-capacities)
	- [List connections](https://learn.microsoft.com/en-us/rest/api/fabric/core/connections/list-connections)
	- [List domains](https://learn.microsoft.com/en-us/rest/api/fabric/admin/domains/list-domains)
	- [List gateways](https://learn.microsoft.com/en-us/rest/api/fabric/core/gateways/list-gateways)
	- [List all items in the tenant](https://learn.microsoft.com/en-us/rest/api/fabric/admin/items/list-items)
	- Worspace inventory:

		- [List workspaces](https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/list-workspaces)
		- List workspaces with details
		- [List workspaces (admin mode)](https://learn.microsoft.com/en-us/rest/api/fabric/admin/workspaces/list-workspaces)
		- Get workspace [details](https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/get-workspace) with [role assignments](https://learn.microsoft.com/en-us/rest/api/fabric/core/workspaces/get-workspace-role-assignment)
		- [Get workspace with access details (admin mode)](https://learn.microsoft.com/en-us/rest/api/fabric/admin/workspaces/list-workspace-access-details)
		- [List git connections](https://learn.microsoft.com/en-us/rest/api/fabric/admin/workspaces/list-git-connections)
		- List role assignements (automate the *get worksapce* command for each workspaces)
		- [List items in a workspace](https://learn.microsoft.com/en-us/rest/api/fabric/core/items/list-items)
		- [Get item access details](https://learn.microsoft.com/en-us/rest/api/fabric/admin/items/list-item-access-details)
