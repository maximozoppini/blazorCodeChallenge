namespace Classes.Commands.Card;

public class CardPost
{
    public string CardHolder { get; set; }
    public string CardNumber { get; set; }
    public int CardExpMonth { get; set; }
    public int CardExpYear { get; set; }
    public string Ccv { get; set; }
    public string Type { get; set; }
}