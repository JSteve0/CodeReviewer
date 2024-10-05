using System.IO;
using CodeReviewer.Logging;
using CodeReviewer.Models;
using Newtonsoft.Json;

namespace CodeReviewer.Services.JsonServices;

public class ProjectDetailsService {
    private const string FilePath = "ProjectInfo.json";
    private readonly ILogger _logger;

    public ProjectDetailsModel ProjectDetails { get; private set; }

    // Constructor using Dependency Injection
    public ProjectDetailsService(ILogger logger) {
        _logger = logger;
        ProjectDetails = LoadProjectDetails(); // Load details on initialization
    }

    // Method to load the entire project details
    private ProjectDetailsModel LoadProjectDetails() {
        string json = File.ReadAllText(FilePath);
        ProjectDetails = JsonConvert.DeserializeObject<ProjectDetailsModel>(json) 
                         ?? throw new InvalidOperationException("Failed to load project details");

        _logger.LogVerbose($"Loaded JSON File {FilePath} with data:\n{ProjectDetails}");
        return ProjectDetails;
    }
    
    public string GetProjectTitle() {
        return ProjectDetails.Title;
    }

    public string GetRepositoryUrl() {
        return ProjectDetails.RepositoryURL;
    }
}

