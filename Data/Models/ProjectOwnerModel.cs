using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lagaltApp;

namespace lagalt.Data.Models
{
  public class ProjectOwnerModel
  {
    public int Id { get; set; }


    public int ProjectId { get; set; }
    public ProjectModel ProjectModel { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }
  }
}