using tickets.Dto;

namespace tickets.Services;

public interface IAdminService
{
    Task<bool> AddEvent(AdminEventDto e);
    Task<bool> DeleteEvent(int eventId);
    Task<bool> ChangeEvent(int eventId, AdminEventDto e);
}