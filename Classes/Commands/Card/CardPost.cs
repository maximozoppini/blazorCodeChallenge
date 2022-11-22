using System.ComponentModel.DataAnnotations;

namespace Classes.Commands.Card;

public class CardPost
{
    [Required(ErrorMessage = "Error CardHolder required")]
    public string CardHolder { get; set; }
    [Required(ErrorMessage = "Error CardNumber required")]
    public string CardNumber { get; set; }
    [Required(ErrorMessage = "Error CardExpMonth required")]
    public int CardExpMonth { get; set; }
    [Required(ErrorMessage = "Error CardExpYear required")]
    public int CardExpYear { get; set; }
    [Required(ErrorMessage = "Error Ccv required")]
    public string Ccv { get; set; }
    public string Type { get; set; }
}