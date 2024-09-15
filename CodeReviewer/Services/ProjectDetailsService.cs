using System.IO;
using CodeReviewer.Models;
using Newtonsoft.Json;

namespace CodeReviewer.Services;

public class ProjectDetailsService {

    private static ProjectDetailsService? _instance;
    private const string FilePath = "ProjectInfo.json";
    
    public ProjectDetailsModel ProjectDetails;

    // Private constructor to prevent instantiation
    private ProjectDetailsService() {
        ProjectDetails = LoadProjectDetails();
    }

    // Public property to access the singleton instance
    public static ProjectDetailsService Instance => _instance ??= new ProjectDetailsService();

    // Method to load the entire project details
    private ProjectDetailsModel LoadProjectDetails() {
        string json = File.ReadAllText(FilePath);
        ProjectDetails = JsonConvert.DeserializeObject<ProjectDetailsModel>(json) ?? throw new InvalidOperationException();
    
        Logging.ConsoleLogger.Instance.LogVerbose($"Loaded JSON File {FilePath} with data:\n" + ProjectDetails.ToString());
        return ProjectDetails;
    }
    
    public string GetProjectTitle() {
        return ProjectDetails.Title;
    }

    public string GetRepositoryUrl() {
        return ProjectDetails.RepositoryURL;
    }

}
