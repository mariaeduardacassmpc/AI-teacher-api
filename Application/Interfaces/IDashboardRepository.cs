namespace Application.Interfaces;

public interface IDashboardRepository
{
    Task<int> GetTotalRequestsAsync();
}