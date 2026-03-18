using Schaden.Domain.Entities.Enums;

namespace Schaden.Domain.Entities
{
    public class Schaden
    {
        public int Id { get; set; }
        public DateTime SchadensEintritt { get; set; }
        public int VersicherungsnehmerId { get; set; }
        public Versicherungsnehmer Versicherungsnehmer { get; set; }
        public List<SchadenPosition> SchadenPositionen { get; set; }
        public SchadenStatus Status { get; set; }
    }

    public class SchadenPosition
    {
        public int Id { get; set; }
        public int SchadenId { get; set; }
        public int VertragId { get; set; }
        public Vertrag Vertrag { get; set; }
        public decimal Schadenshoehe { get; set; }
        public string Beschreibung { get; set; }
    }

    public class Versicherungsnehmer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Kundennummer { get; set; }
    }

    public class Vertrag
    {
        public int Id { get; set; }
        public string Vertragsnummer { get; set; }
        public string VertragsArt { get; set; } // z.B. "Haftpflicht", "Hausrat", "KFZ"
        public decimal Praemie { get; set; }
    }
}
