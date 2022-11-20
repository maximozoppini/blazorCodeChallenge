namespace Classes.DTO.Card;

public class CardsDTO : BaseResult
{
    public List<CardItem> Cards { get; set; } = new List<CardItem>();
}

public class CardItem
{
    public Guid Id { get; set; }
    public string CardHolder { get; set; }
    public int CardNumber { get; set; }
    public int CardExpMonth { get; set; }
    public int CardExpYear { get; set; }
    public int Ccv { get; set; }
    public string Type { get; set; }
}