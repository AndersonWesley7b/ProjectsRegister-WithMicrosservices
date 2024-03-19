﻿using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS.Interfaces;

namespace ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
public sealed class CreateProjectDTO : IProjectDTO
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ProjectLink { get; set; } = string.Empty;

    public string RepositoryLink { get; set; } = string.Empty;

    public string MediaLink { get; set; } = string.Empty;

    public Guid UserId { get; set; }
}
