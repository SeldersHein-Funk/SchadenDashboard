using Microsoft.EntityFrameworkCore;
using Schaden.Infrastructure;

namespace Schaden.Application
{
    public class SchadenReportService
    {
        private readonly SchadenDbContext _context;

        public SchadenReportService(SchadenDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardReport> GetSchadenDashboard(DateTime from, DateTime to)
        {
            // Holt alle Schadensfälle im Zeitraum
            var schaden = await _context.Schaeden
                .Include(s => s.SchadenPositionen)
                    .ThenInclude(sp => sp.Vertrag)
                .Where(s => s.SchadensEintritt >= from && s.SchadensEintritt <= to)
                .ToListAsync();

            // Berechnet Statistiken
            var totalSchadenSumme = schaden.Sum(s => s.SchadenPositionen.Sum(sp => sp.Schadenshoehe));
            var totalSchaden = schaden.Count;
            var avgSchadenhoehe = totalSchadenSumme / totalSchaden;

            // Top 5 Vertragsarten mit höchster Schadensumme
            var topVertragsarten = schaden
                .SelectMany(s => s.SchadenPositionen)
                .GroupBy(sp => sp.Vertrag.VertragsArt)
                .Select(g => new VertragsartStats
                {
                    VertragsArt = g.Key,
                    AnzahlSchaden = g.Count(),
                    GesamtSchadenSumme = g.Sum(sp => sp.Schadenshoehe)
                })
                .OrderByDescending(v => v.GesamtSchadenSumme)
                .Take(5)
                .ToList();

            // Top 10 Versicherungsnehmer nach Schadensumme
            var topVersicherungsnehmer = schaden
                .GroupBy(s => s.Versicherungsnehmer)
                .Select(g => new VersicherungsnehmerStats
                {
                    VersicherungsnehmerName = g.Key.Name,
                    AnzahlSchaden = g.Count(),
                    GesamtSchadenSumme = g.Sum(s => s.SchadenPositionen.Sum(sp => sp.Schadenshoehe))
                })
                .OrderByDescending(v => v.GesamtSchadenSumme)
                .Take(10)
                .ToList();

            return new DashboardReport
            {
                TotalSchadenSumme = totalSchadenSumme,
                TotalSchaden = totalSchaden,
                DurchschnittlicheSchadenshoehe = avgSchadenhoehe,
                TopVertragsarten = topVertragsarten,
                TopVersicherungsnehmer = topVersicherungsnehmer
            };
        }
    }

    public class DashboardReport
    {
        public decimal TotalSchadenSumme { get; set; }
        public int TotalSchaden { get; set; }
        public decimal DurchschnittlicheSchadenshoehe { get; set; }
        public List<VertragsartStats> TopVertragsarten { get; set; }
        public List<VersicherungsnehmerStats> TopVersicherungsnehmer { get; set; }
    }

    public class VertragsartStats
    {
        public string VertragsArt { get; set; }
        public int AnzahlSchaden { get; set; }
        public decimal GesamtSchadenSumme { get; set; }
    }

    public class VersicherungsnehmerStats
    {
        public string VersicherungsnehmerName { get; set; }
        public int AnzahlSchaden { get; set; }
        public decimal GesamtSchadenSumme { get; set; }
    }
}