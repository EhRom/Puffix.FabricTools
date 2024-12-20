﻿using Puffix.FabricTools.ConsoleApp.Domain.Workspaces.Models;

namespace Puffix.FabricTools.ConsoleApp.Domain.Workspaces;

public interface IWorkspacesService
{
    Task<IWorkspaceCommandResult<WorkspaceList>> List();

    Task<IWorkspaceCommandResult<Workspace>> GetWorkspaceDetails(string workspaceId);

    Task<IWorkspaceCommandResult<WorkspaceList>> ListWithDetails();

    Task<IWorkspaceCommandResult<WorkspaceList>> ListWithRoleAssignements();

    Task<IWorkspaceCommandResult<FabricItemList>> GetWorkspaceItems(string workspaceId);

    Task<IWorkspaceCommandResult<Workspace>> AssignWorkspaceToCapacity(string capacityId, string workspaceId);

    Task<IWorkspaceCommandResult<WorkspaceList>> AssignWorkspaceCollectionToCapacity(string queryFilePath);

    Task<IWorkspaceCommandResult<Workspace>> UnassignWorkspaceToCapacity(string workspaceId);

    Task<IWorkspaceCommandResult<WorkspaceList>> UnassignWorkspaceCollectionToCapacity(string queryFilePath);
}