using backend.Data;
using backend.Models.Chore.CompletedChore;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Utilities;

public class XpHandler
{
    private DateTime _today;

    public XpHandler()
    {
        _today = DateTime.Now;
    }

    public async Task<CalculatedXp> CalculateXpValues(DataContext context, int userId)
    {
        List<CompletedChoreEntity> choresCompletedToday = new List<CompletedChoreEntity>();
        List<CompletedChoreEntity> choresCompletedThisMonth = new List<CompletedChoreEntity>();
        List<CompletedChoreEntity> choresCompletedLastMonth = new List<CompletedChoreEntity>();
        List<CompletedChoreEntity> completedChores;

        var lastMonthStart = new DateTime(_today.Date.Year, _today.Date.AddMonths(-1).Month, 1);
        var lastMonthEnd = lastMonthStart.AddMonths(1).AddSeconds(-1);
        var thisMounthStart = new DateTime(_today.Date.Year, _today.Date.Month, 1);

        var response = new CalculatedXp();

        completedChores = await context.CompletedChores.ToListAsync();

        foreach (CompletedChoreEntity chore in completedChores)
        {
            if (chore.UserId != userId) continue;

            if (chore.DateCompleted >= _today.Date)
            {
                choresCompletedToday.Add(chore);
            }

            if (chore.DateCompleted >= thisMounthStart)
            {
                choresCompletedThisMonth.Add(chore);
            }

            if (chore.DateCompleted >= lastMonthStart && chore.DateCompleted <= lastMonthEnd)
            {
                choresCompletedLastMonth.Add(chore);
            }


        }

        choresCompletedToday.ForEach(chore =>
        {
            response.XpToday += chore.CompletedXp;
        });

        choresCompletedThisMonth.ForEach(chore =>
        {
            response.XpThisMonth += chore.CompletedXp;
        });

        choresCompletedLastMonth.ForEach(chore =>
        {
            response.XpLastMonth += chore.CompletedXp;
        });


        return response;

    }
}

public class CalculatedXp
{
    public int XpToday { get; set; } = 0;
    public int XpLastMonth { get; set; } = 0;
    public int XpThisMonth { get; set; } = 0;
}
