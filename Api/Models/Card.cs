using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public int CardExpMonth { get; set; }
        public int CardExpYear { get; set; }
        public string Ccv { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
