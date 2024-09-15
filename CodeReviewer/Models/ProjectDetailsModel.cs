﻿using Newtonsoft.Json;

namespace CodeReviewer.Models;

public class ProjectDetailsModel {
    
    [JsonProperty]
    public string Title { get; private set; }
    [JsonProperty]
    public string Description { get; private set; }
    [JsonProperty]
    public string Version { get; private set; }
    [JsonProperty]
    public string RepositoryURL { get; private set; }
    [JsonProperty]
    public string LicenseURL { get; private set; }
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public ProjectDetailsModel(
        string projectTitle, 
        string projectDescription, 
        string projectVersion, 
        string projectRepositoryURL,
        string projectLicenseURL) {
        Title = projectTitle;
        Description = projectDescription;
        Version = projectVersion;
        RepositoryURL = projectRepositoryURL;
        LicenseURL = projectLicenseURL;
    }
    
    public override string ToString() {
        return $"Title: {Title}\nDescription: {Description}\nVersion: {Version}\nRepository URL: {RepositoryURL}\nLicense URL: {LicenseURL}";
    }
}
