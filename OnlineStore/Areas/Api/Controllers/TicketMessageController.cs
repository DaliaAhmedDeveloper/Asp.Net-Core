namespace OnlineStore.Areas.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models;
using OnlineStore.Helpers;
using OnlineStore.Models.Dtos.Responses;

[Area("Api")]
[Authorize(AuthenticationSchemes = "Api")]
[Route("[area]")]
[ApiController]
public class TicketMessageController : ControllerBase
{
    private readonly ITicketMessageService _TicketMessage;
    public TicketMessageController(ITicketMessageService TicketMessage)
    {
        _TicketMessage = TicketMessage;
    }
    // add new TicketMessage
    [HttpPost("ticketMessage")]
    public async Task<IActionResult> Add(CreateTicketMessageDto dto)
    {
        var TicketMessage = await _TicketMessage.CreateTicketMessageAsync(dto);
        return Ok(ApiResponseHelper<TicketMessageDto>.Success(TicketMessage.ToDto(), ""));
    }
    // get all ticket message 
    [HttpGet ("ticket/{ticketId}/messages")]
    public async Task<IActionResult> TicketMessages(int ticketId)
    {
        var ticketMessages = await _TicketMessage.ListByTicket(ticketId);
        var mapper = MapperHelper.TicketMessageList(ticketMessages);
        return Ok(ApiResponseHelper<TicketMessageDto>.CollectionSuccess(mapper, ""));
    }
}