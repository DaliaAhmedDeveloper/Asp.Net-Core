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
public class SupportTicketController : ControllerBase
{
    private readonly ISupportTicketService _SupportTicket;
    public SupportTicketController(ISupportTicketService SupportTicket)
    {
        _SupportTicket = SupportTicket;
    }
    // add new SupportTicket
    [HttpPost("supportTicket")]
    public async Task<IActionResult> Add(CreateSupportTicketDto dto)
    {
        var SupportTicket = await _SupportTicket.CreateSupportTicketAsync(dto);
        return Ok(ApiResponseHelper<SupportTicketDto>.Success(SupportTicket.ToDto(), ""));
    }
    // get all order support tickets
    [HttpGet("order/{orderId}/supportTickets")]
    public async Task<IActionResult> OrderTickets(int orderId)
    {
        var supportTickets = await _SupportTicket.ListByOrder(orderId);
        var mapper = MapperHelper.TicketList(supportTickets);
        return Ok(ApiResponseHelper<SupportTicketDto>.CollectionSuccess(mapper, ""));
    }
    // get all user support tickets
     [HttpGet("user/{userId}/supportTickets")]
    public async Task<IActionResult> UserTickets(int userId)
    {
        var supportTickets = await _SupportTicket.ListByUser(userId);
        var mapper = MapperHelper.TicketList(supportTickets);
        return Ok(ApiResponseHelper<SupportTicketDto>.CollectionSuccess(mapper, ""));
    }
}