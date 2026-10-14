using Application.Interfaces;
using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DashboardRepository(AITeacherDbContext context) : IDashboardRepository
{
    public async Task<int> GetTotalRequestsAsync()
    {
        return await context.CreditRequests.CountAsync();
    }
}