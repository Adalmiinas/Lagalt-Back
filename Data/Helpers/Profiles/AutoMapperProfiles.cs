using AutoMapper;
using lagaltApp;

namespace lagalt
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<UserModel, RegisterAppUserDto>().ReverseMap();
      CreateMap<UserModel, LoginDto>().ReverseMap();
      CreateMap<UserModel, UserDto>().ReverseMap();


      //skills
    CreateMap<SkillModel, SkillDto>().ReverseMap();

    //project
    CreateMap<ProjectModel, ProjectDto>().ReverseMap();
    // CreateMap<SkillModel, SkillDto>().ReverseMap();
    }
  }
}