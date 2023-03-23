using Microsoft.AspNetCore.Mvc;

namespace Lagalt
{
  public interface IMessageBoardRepository
  {
    Task<ActionResult<MessageBoardDto>> CreateMessageBoardAsync(int userId, CreateMessageBoardDto createMessageBoard);
    Task<ActionResult<List<MessageBoardDto>>> ReadMessageBoardAsync(int ProjectId);
    Task<ActionResult<MessageBoardDto>> UpdateMessageBoardAsync(int userId, UpdateMessageBoardDto updateMessageBoard);
    Task<ActionResult<MessageBoardDto>> DeleteMessageBoardAsync(int userId, DeleteMessageBoardDto deleteMessageBoard);
  }
}