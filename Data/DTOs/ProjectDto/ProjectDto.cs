using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lagaltApp;

namespace lagalt
{
  public class ProjectDto
  {
    public int Id { get; set; }


    public int OwnerId { get; set; }
    public UserDto Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }
  }
}