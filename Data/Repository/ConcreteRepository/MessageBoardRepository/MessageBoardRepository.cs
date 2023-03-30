using AutoMapper;
using lagalt;
using lagaltApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lagalt
{
  public class MessageBoardRepository : IMessageBoardRepository
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public MessageBoardRepository(DataContext dataContext, IMapper mapper)
    {
      _mapper = mapper;
      _dataContext = dataContext;
    }
   /// <summary>
   /// Create message board
   /// </summary>
   /// <param name="userId"></param>
   /// <param name="createMessageBoard"></param>
   /// <returns></returns>
    public async Task<ActionResult<MessageBoardDto>> CreateMessageBoardAsync(int userId, CreateMessageBoardDto createMessageBoard)
    {
      //check user id > and also check if user in project 
      var IsUser = await _dataContext.ProjectUsers.Include(p => p.Project).Include(u => u.User).FirstOrDefaultAsync(pu => pu.UserId == userId);
      var Project = await _dataContext.Projects.FirstOrDefaultAsync(p => p.Id == createMessageBoard.ProjectId);
      if (IsUser == null)
      {
        return new BadRequestObjectResult("User id not found, user may not be part of the project");
      }
      // //add message board in the list of project msgb list
      var newMessage = _mapper.Map<MessageBoardModel>(createMessageBoard);
      newMessage.Username = IsUser.User.Username;
      newMessage.UserId = IsUser.UserId;

      Project.MessageBoards.Add(newMessage);
      // _dataContext.Entry(Project).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();

      return new CreatedResult("CreateMessageBoard", "Message Created Successfully");

    }

    /// <summary>
    /// delete message
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deleteMessageBoard"></param>
    /// <returns></returns>
    public async Task<ActionResult<MessageBoardDto>> DeleteMessageBoardAsync(int userId, DeleteMessageBoardDto deleteMessageBoard)
    {
      var findMessage = await _dataContext.MessageBoards.FirstOrDefaultAsync(mb => mb.Id == deleteMessageBoard.MessageBoardId && mb.UserId == userId);
      if (findMessage == null) return new BadRequestObjectResult("User cannot remove that Specific Message");

      _dataContext.MessageBoards.Remove(findMessage);
      await _dataContext.SaveChangesAsync();
      return new NoContentResult();
    }
    /// <summary>
    /// get message boards
    /// </summary>
    /// <param name="ProjectId"></param>
    /// <returns></returns>
    public async Task<ActionResult<List<MessageBoardDto>>> ReadMessageBoardAsync(int ProjectId)
    {
      var messages = await _dataContext.MessageBoards.Where(p => p.ProjectId == ProjectId).ToListAsync();
      if (messages == null) return new BadRequestObjectResult("Incorrect id");

      return new OkObjectResult(_mapper.Map<List<MessageBoardDto>>(messages));
    }
    /// <summary>
    /// Update 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="updateMessageBoard"></param>
    /// <returns></returns>
    public async Task<ActionResult<MessageBoardDto>> UpdateMessageBoardAsync(int userId, UpdateMessageBoardDto updateMessageBoard)
    {
      //check user id > and also check if user in project 
      var IsUser = await _dataContext.ProjectUsers.Include(p => p.Project).Include(u => u.User).FirstOrDefaultAsync(pu => pu.UserId == userId);
      //correct project
      var Project = await _dataContext.Projects.FirstOrDefaultAsync(p => p.Id == updateMessageBoard.ProjectId);
      //the message we want to update
      var message = await _dataContext.MessageBoards.FirstOrDefaultAsync(m => m.Id == updateMessageBoard.messageboardId && m.UserId == userId);

      if (IsUser == null) return new BadRequestObjectResult("User id not found, user may not be part of the project");
      if (Project == null) return new BadRequestObjectResult("project id not found, user may not be part of the project");
      if (message == null) return new BadRequestObjectResult("message id not found, user may not be part of the project");

      var UpdatedMessage = new MessageBoardDto
      {
        Id = message.Id,
        UserId = IsUser.UserId,
        Username = IsUser.User.Username,
        Title = updateMessageBoard.Title == null ? message.Title : updateMessageBoard.Title,
        Body = updateMessageBoard.Body == null ? message.Body : updateMessageBoard.Body,
        ProjectId = Project.Id

      };
      _mapper.Map(UpdatedMessage, message);
      _dataContext.Entry(message).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();
      return new OkObjectResult("Updated");
    }
  }
}