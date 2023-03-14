using AutoMapper;
using Lagalt;
using lagaltApp;
using Microsoft.AspNetCore.DataProtection;

namespace lagalt
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      //login
      CreateMap<UserModel, RegisterAppUserDto>().ReverseMap();
      CreateMap<UserModel, LoginDto>().ReverseMap();
      //user misc

      CreateMap<UserModel, UserDto>().ReverseMap();
      CreateMap<List<UserModel>, UserDto>().ReverseMap();
      CreateMap<UserModel, UserNameDto>().ReverseMap();
      CreateMap<UserDto, UserNameDto>().ReverseMap();

      CreateMap<UserModel, List<ProjectUserModel>>().ReverseMap();
      CreateMap<ProjectModel, List<ProjectUserModel>>().ReverseMap();

      //skills
      CreateMap<SkillModel, SkillDto>().ReverseMap();
      CreateMap<SkillModel, SkillNameDto>().ReverseMap();
      CreateMap<List<SkillDto>, SkillModel>().ReverseMap();
      CreateMap<SkillDto, SkillNameDto>().ReverseMap();



      //project image 
      CreateMap<ProjectImageDto, ProjectImageModel>();
      CreateMap<ProjectImageModel, ProjectImageDto>();

      //project
      CreateMap<ProjectModel, ProjectDto>().ReverseMap()
      .ForMember(dest => dest.WaitList, opt => opt.MapFrom(src => src.WaitList));
      CreateMap<ProjectModel, UpdateProjectDetailsDto>().ReverseMap();
      CreateMap<ProjectDto, ProjectModel>();


      //Project list
      CreateMap<ProjectModel, ProjectListDto>().ReverseMap();



      CreateMap<ProjectSkillListDto, SkillModel>().ReverseMap()
      .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.SkillName));

      CreateMap<ProjectListUserDto, ProjectUserModel>().ReverseMap()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));

      CreateMap<ProjectListUserDto, UserModel>().ReverseMap()
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));


      CreateMap<SkillNameDto, SkillModel>().ReverseMap();


      CreateMap<ProjectModel, CreateProjectDto>().ReverseMap();

      CreateMap<CreateProjectDto, ProjectModel>();


      CreateMap<ProjectDto, CreateProjectDto>().ReverseMap();

      //project userr get users projects for profile page

      CreateMap<ProjectUserModel, ProjectUserDto>();
      CreateMap<UserModel, ProjectUserDto>()
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
      CreateMap<ProjectUserDto, ProjectUserModel>();
      CreateMap<UserModel, ProjectUserModel>()
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

      //update project details
      CreateMap<UpdateProjectDetailsDto, ProjectModel>()
      .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TagNames))
      .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.SkillNames))
      .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
      .ForMember(dest => dest.projectImage, opt => opt.MapFrom(src => src.ProjectImage));


      //Industry
      CreateMap<IndustryModel, IndustryDto>().ReverseMap();
      CreateMap<IndustryModel, IndustryNameDto>().ReverseMap();
      CreateMap<IndustryDto, IndustryNameDto>().ReverseMap();

      CreateMap<IndustryNameDto, IndustryModel>()
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.IndustryName));
      CreateMap<IndustryModel, UpdateProjectDetailsDto>().ReverseMap();
      // .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Industry.IndustryName));


      CreateMap<IndustryModel, IndustryNameDto>()
      .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src.Name));

      //tags
      CreateMap<TagModel, TagDto>().ReverseMap();

      CreateMap<TagModel, TagNameDto>().ReverseMap();
      CreateMap<TagDto, TagNameDto>().ReverseMap();
      CreateMap<TagNameDto, TagModel>().ReverseMap()
      .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.TagName));

      //userinwaitinglist
      CreateMap<UserInWaitingListDto, ProjectUserModel>();

      CreateMap<UserInWaitingListModel, UserModel>()
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

      CreateMap<WaitListModel, UserInWaitingListModel>()
      .ForMember(dest => dest.WaitListId, opt => opt.MapFrom(src => src.Id));
      CreateMap<UserInWaitingListModel, ProjectModel>();
      //  .ForMember(dest => dest, opt => opt.Ignore());
      //  .ForMember(dest => dest., opt => opt.MapFrom(src => src.Id))

      CreateMap<UserModel, UserInWaitingListModel>()
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.PendingStatus, opt => opt.Ignore());
      //  .ForMember(dest => dest.PendingStatus, opt => opt.Ignore());
      // .ForMember(dest => dest.PendingStatus, opt)

      //update user
      CreateMap<UserModel, UpdateAppUserDto>();
      CreateMap<UpdateAppUserDto, UserModel>();


      CreateMap<WaitListModel, UserModel>().ReverseMap();
      CreateMap<WaitListModel, ProjectModel>().ReverseMap();
      CreateMap<ProjectModel, ProjectDto>().ReverseMap();
      CreateMap<ProjectDto, WaitListModel>().ReverseMap();
      CreateMap<ProjectDto, WaitListDto>().ReverseMap();
      CreateMap<WaitListModel, WaitListDto>().ReverseMap();
      CreateMap<UserInWaitingListModel, UserInWaitingListDto>().ReverseMap();

      CreateMap<WaitListModel, UserInWaitingListModel>().ReverseMap();


      //      .ForMember(dest => dest.UsersInWaitingLists, opt => opt.MapFrom(src => src.WaitList.UserWaitingLists));


    }
  }
}