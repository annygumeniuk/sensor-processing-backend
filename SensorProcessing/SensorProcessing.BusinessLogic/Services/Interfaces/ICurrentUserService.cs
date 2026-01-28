namespace SensorProcessing.BusinessLogic.Services.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? GetUserId();
        string? GetEmail();
        bool IsAuthenticated();
    }
}