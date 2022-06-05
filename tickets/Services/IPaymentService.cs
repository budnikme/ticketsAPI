using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public interface IPaymentService
{
    Task<ServiceResponse<TicketsDto>> BuyTicket(int ticketTypeId, int userId);
    Task<ServiceResponse<CardDataDto>> AddCard(CardDto card, int userId);
    Task<ServiceResponse<bool>> DeleteCard(int cardId, int userId);
    Task<ServiceResponse<List<CardDataDto>>> UserCards(int userId);
}