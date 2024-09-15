namespace CodeReviewer.Models;

public class ProjectInfo {

    private const string JsonFileName = "ProjectInfo.json";
    
    
    public string ProjectName { get; set; } = "";
    public string ProjectPath { get; set; } = "";
    public string ProjectType { get; set; } = "";
    public string ProjectLanguage { get; set; } = "";
    
    public ProjectInfo() {
    }
    
    public ProjectInfo(string projectName, string projectPath, string projectType, string projectLanguage) {
        ProjectName = projectName;
        ProjectPath = projectPath;
        ProjectType = projectType;
        ProjectLanguage = projectLanguage;
    }
    
    public static ProjectInfo LoadProjectInfo() {
        if (File.Exists(JsonFileName)) {
            string json = File.ReadAllText(JsonFileName);
            return JsonSerializer.Deserialize<ProjectInfo>(json);
        }
        return new ProjectInfo();
    }
    
    public void SaveProjectInfo() {
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(JsonFileName, json);
    }
}
