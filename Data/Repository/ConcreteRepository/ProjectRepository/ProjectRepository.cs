using AutoMapper;
using Lagalt;
using lagaltApp;
using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Create project and set sender as project owner
    /// and add project owner as member of project
    /// </summary>
    /// <param name="id"></param>
    /// <param name="createProjectDto"></param>
    /// <returns></returns>
    public async Task<IActionResult> CreateProjectAsync(int id, CreateProjectDto createProjectDto)
    {
      var existingSkills = await _dataContext.skills
      .Where(s => createProjectDto.SkillNames
      .Select(sm => sm.SkillName).Contains(s.SkillName)).ToListAsync();

      var existingTags = await _dataContext.Tags
      .Where(t => createProjectDto.TagNames
      .Select(tn => tn.TagName).Contains(t.TagName)).ToListAsync();

      var exisitingUser = await _dataContext.Users.FindAsync(id);
      if (exisitingUser == null) return new BadRequestObjectResult("Incorrect id / Id does not exist");


      var newProject = _mapper.Map<ProjectModel>(createProjectDto);
      newProject.Skills = new List<SkillModel>();
      newProject.Tags = new List<TagModel>();
      newProject.WaitList = new WaitListModel();

      var existingIndustry = await _dataContext.Industries.FirstOrDefaultAsync(i => i.Name == createProjectDto.IndustryName.IndustryName);

      newProject.Industry = existingIndustry != null ? existingIndustry : _mapper.Map<IndustryModel>(createProjectDto.IndustryName);

      var newProjectOwner = new ProjectUserModel()
      {
        IsOwner = true,
        UserId = exisitingUser.Id,
      };

      newProject.ProjectUsers.Add(newProjectOwner);

      foreach (var skill in createProjectDto.SkillNames)
      {
        var existingSkill = existingSkills.FirstOrDefault(es => es.SkillName == skill.SkillName);
        newProject.Skills.Add(existingSkill != null ? existingSkill : _mapper.Map<SkillModel>(skill));
      }
      foreach (var Tag in createProjectDto.TagNames)
      {
        var existingTag = existingTags.FirstOrDefault(et => et.TagName == Tag.TagName);
        newProject.Tags.Add(existingTag != null ? existingTag : _mapper.Map<TagModel>(Tag));
      }
      _dataContext.Projects.Add(newProject);
      await _dataContext.SaveChangesAsync();

      var newProj = _mapper.Map<CreateProjectDto>(createProjectDto);
      return new CreatedAtRouteResult(nameof(CreateProjectAsync), newProj);
    }

    /// <summary>
    /// Get singular project matching the id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ActionResult<ProjectDto>> GetProjectAsync(int id)
    {
      var findProject = await _dataContext.Projects
      .Include(pu => pu.ProjectUsers)
      .ThenInclude(u => u.User)
       .Include(p => p.WaitList)
       .ThenInclude(w => w.UserWaitingLists)
      .Include(s => s.Skills)
      .Include(i => i.Industry)
      .Include(t => t.Tags)
      .FirstOrDefaultAsync(p => p.Id == id);

      if (findProject == null) return new BadRequestObjectResult("Bad id");

      

      return new OkObjectResult(_mapper.Map<ProjectDto>(findProject));
    }

    /// <summary>
    /// Get  projects matching the name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<ActionResult<List<ProjectListDto>>> GetProjectsAsync(string name)
    {
      var findProject = await _dataContext.Projects
      .Include(pu => pu.ProjectUsers)
      .ThenInclude(u => u.User)
      .Include(s => s.Skills)
      .Include(i => i.Industry)
      .Include(t => t.Tags)
      .Where(pn => pn.Title == name).ToListAsync();

      if (findProject.ToArray().Length <= 0) return new EmptyResult();

      return new OkObjectResult(_mapper.Map<List<ProjectListDto>>(findProject));
    }

    /// <summary>
    /// Get list of all projects
    /// </summary>
    /// <returns></returns>
    public async Task<ActionResult<List<ProjectListDto>>> GetProjectsAsync()
    {
      var findAll = await _dataContext.Projects
      .Include(pu => pu.ProjectUsers)
      .ThenInclude(u => u.User)
      .Include(s => s.Skills)
      .Include(i => i.Industry)
      .Include(t => t.Tags)
      .ToListAsync();

      if (findAll.ToArray().Length <= 0)
      {
        return new EmptyResult();
      }
      else
      {
        return new OkObjectResult(_mapper.Map<List<ProjectListDto>>(findAll));
      }
    }
    /// <summary>
    /// Update project details but not member list, chat etc. 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateProjectDto"></param>
    /// <returns></returns>
    public async Task<IActionResult> UpdateProjectAsync(int id, UpdateProjectDetailsDto updateProjectDto)
    {
      var existingTags = await _dataContext.Tags.ToListAsync();
      var existingSkills = await _dataContext.skills.ToListAsync();
      var existingIndustries = await _dataContext.Industries.ToListAsync();

      //list of tag names
      //get project to be updated
      var existingProject = await _dataContext.Projects
      .Include(pu => pu.ProjectUsers)
      .Include(s => s.Skills)
      .Include(i => i.Industry)
      .Include(t => t.Tags)
      .Include(p => p.projectImage)
      .FirstOrDefaultAsync(pu => pu.Id == updateProjectDto.Id);

      var IsOwner = await _dataContext.ProjectUsers.FirstOrDefaultAsync(pu => pu.UserId == id && pu.IsOwner == true && existingProject.Id == pu.ProjectId);
      // var existingProject = await _dataContext.Projects.FindAsync(existingProject.ProjectId);
      if (IsOwner == null) throw new Exception("Incorrect Id, you do not have right to modify this project ");

      var UpdateDetails = new UpdateProjectDetailsDto
      {
        Id = existingProject.Id,
        Title = updateProjectDto.Title == null ? existingProject.Title : updateProjectDto.Title,
        Description = updateProjectDto.Description == null ? existingProject.Description : updateProjectDto.Description,
        GitRepositoryUrl = updateProjectDto.GitRepositoryUrl == null ? existingProject.GitRepositoryUrl : updateProjectDto.Description,
        ProjectImage = updateProjectDto.ProjectImage == null ? _mapper.Map<ProjectImageDto>(existingProject.projectImage) : updateProjectDto.ProjectImage,
        Industry = new IndustryNameDto(),
        TagNames = new List<TagNameDto>(),
        SkillNames = new List<SkillNameDto>()
      };

      foreach (var tag in updateProjectDto.TagNames)
      {
        //if doesnt find then create new else add existing
        var isNewTag = await _dataContext.Tags.FirstOrDefaultAsync(t => t.TagName == tag.TagName);
        UpdateDetails.TagNames.Add(isNewTag == null ? _mapper.Map<TagNameDto>(tag) : _mapper.Map<TagNameDto>(isNewTag));
      }

      foreach (var skill in updateProjectDto.SkillNames)
      {
        var isNewSkill = existingSkills.FirstOrDefault(p => p.SkillName == skill.SkillName);
        UpdateDetails.SkillNames.Add(isNewSkill == null ? _mapper.Map<SkillNameDto>(skill) : _mapper.Map<SkillNameDto>(isNewSkill));
      }
      var isNewIndustry = await _dataContext.Industries.FirstOrDefaultAsync(i => i.Name == updateProjectDto.Industry.IndustryName);
      UpdateDetails.Industry = isNewIndustry != null ? _mapper.Map<IndustryNameDto>(isNewIndustry) : _mapper.Map<IndustryNameDto>(updateProjectDto.Industry);

      _mapper.Map(UpdateDetails, existingProject);

      _dataContext.Entry(existingProject).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();
      return new OkObjectResult(_mapper.Map<UpdateProjectDetailsDto>(UpdateDetails));
    }
  }
}


