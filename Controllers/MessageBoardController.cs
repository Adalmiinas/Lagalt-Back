using System.Reflection.Metadata.Ecma335;
using lagalt.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Lagalt
{
  public class MessageBoardController : BaseApiController
  {
    private readonly IMessageBoardRepository _messageBoardRepository;
    public MessageBoardController(IMessageBoardRepository messageBoardRepository)
    {
      _messageBoardRepository = messageBoardRepository;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<MessageBoardDto>> CreateMessageBoardAsync([FromHeader] int userId, [FromBody] CreateMessageBoardDto createMessageBoard)
    {
      return await _messageBoardRepository.CreateMessageBoardAsync(userId, createMessageBoard);
    }
    [HttpGet]
    public async Task<ActionResult<List<MessageBoardDto>>> GetMessageBoardsAsync([FromHeader] int projectId)
    {
      return await _messageBoardRepository.ReadMessageBoardAsync(projectId);
    }
    [HttpPut]
    public async Task<ActionResult<MessageBoardDto>> UpdateMessageBoardAsync([FromHeader] int userId, [FromBody] UpdateMessageBoardDto updateMessageBoard)
    {
      return await _messageBoardRepository.UpdateMessageBoardAsync(userId, updateMessageBoard);
    }
    [HttpDelete]
    public async Task<ActionResult<MessageBoardDto>> DeleteMessageBoardAsync([FromHeader] int userId, [FromBody] DeleteMessageBoardDto deleteMessageBoard)
    {
      return await _messageBoardRepository.DeleteMessageBoardAsync(userId, deleteMessageBoard);
    }
  }
}