namespace LBD.Classes
{
    public class CardGameOptionsBuilder
    {
        private CardGameOptions _options;
        public CardGameOptionsBuilder()
        {
            this._options = new CardGameOptions();
        }

        public CardGameOptionsBuilder DiscardToGraveyard()
        {
            this._options.DiscardToGraveyard = true;
            return this;
        }

        public CardGameOptionsBuilder DiscardToDiscardPile()
        {
            this._options.DiscardToDiscardPile = true;
            return this;
        }

        public CardGameOptionsBuilder DrawOnTurnStart()
        {
            this._options.DrawOnTurnStart = true;
            this._options.DrawOnPlayerTurnStart = false;
            return this;
        }

        public CardGameOptionsBuilder DrawOnPlayerTurnStart()
        {
            this._options.DrawOnPlayerTurnStart = true;
            this._options.DrawOnTurnStart = false;
            return this;
        }

        public CardGameOptionsBuilder MaxHandSize(int maxHandSize)
        {
            this._options.MaxHandSize = maxHandSize;
            return this;
        }

        public CardGameOptionsBuilder MaxFieldSize(int maxFieldSize)
        {
            this._options.MaxFieldSize = maxFieldSize;
            return this;
        }

        public CardGameOptionsBuilder MaxActiveDeckSize(int maxActiveDeckSize)
        {
            this._options.MaxActiveDeckSize = maxActiveDeckSize;
            return this;
        }

        public CardGameOptions Build()
        {
            return this._options;
        }
    }
}