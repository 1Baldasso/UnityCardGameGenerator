namespace LBD.Classes
{
    public class CardGameOptions
    {
        public bool DiscardToGraveyard { get; set; }
        public bool DiscardToDiscardPile { get; set; }
        public bool DrawOnTurnStart { get; set; }
        public bool DrawOnPlayerTurnStart { get; set; }
        public int MaxHandSize { get; set; }
        public int MaxFieldSize { get; set; }
        public int MaxActiveDeckSize { get; set; }
    }
}