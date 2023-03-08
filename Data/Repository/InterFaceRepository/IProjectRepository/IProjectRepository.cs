namespace lagalt
{
  public interface IProjectRepository
  {
    Task<List<ProjectDto>> GetProjectsAsync();
    Task<ProjectDto> GetProjectAsync(int id);
  }
}