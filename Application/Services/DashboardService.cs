using Application.Dtos.Dashboard;
using Application.Interfaces;

namespace Application.Services;

public class DashboardService(IDashboardRepository repository)
{
    public async Task<CreditDashboardDto> GetDashboard()
    {
        var totalRequests = await repository.GetTotalRequestsAsync();

        return new CreditDashboardDto
        {
            TotalRequests = totalRequests,

        };
    }
}