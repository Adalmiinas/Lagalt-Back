using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace lagalt
{
  public class ProjectRepository : IProjectRepository
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public ProjectRepository(DataContext dataContext, IMapper mapper)
    {
      _mapper = mapper;
      _dataContext = dataContext;
    }

    public async Task<ProjectDto> GetProjectAsync(int id)
    {
      var findProject = await _dataContext.Projects
      .Include(p => p.Owner)

      .FirstOrDefaultAsync(p => p.Id == id);

      if (findProject == null) throw new Exception(message: "Bad id");


      return _mapper.Map<ProjectDto>(findProject);
    }

    public async Task<List<ProjectDto>> GetProjectsAsync()
    {
      var findAll = await _dataContext.Projects.ToListAsync();
      if (findAll == null) throw new Exception(message: "no projects exist, create new projects!");
      return _mapper.Map<List<ProjectDto>>(findAll);

    }
  }
}