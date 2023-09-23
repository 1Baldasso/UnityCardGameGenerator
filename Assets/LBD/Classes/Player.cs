using System;
using System.Collections.Generic;
using System.Linq;
using LBD.Enums;
using LBD.Managers;

namespace LBD.Classes
{
    public class Player
    {
        protected Guid PlayerId;

        /// <summary>
        /// Called whenever the player plays a card
        /// </summary>
        public event Action<Card> OnPlayCard;

        /// <summary>
        /// Called whenever the player draws a card
        /// </summary>
        public event Action<Card> OnCardDrawn;

        /// <summary>
        /// Called whenever a card leaves the player's hand
        /// </summary>
        public event Action<Card> OnCardLeftHand;

        /// <summary>
        /// Called whenever a card leaves the player's field
        /// </summary>
        public event Action<Card> OnCardLeftField;
        
        /// <summary>
        /// Called whenever the a card is discarded by any means
        /// </summary>
        public event Action<Card> OnCardDiscarded;
        
        /// <summary>
        /// Called whenever a card is burned
        /// </summary>
        public event Action<Card> OnCardBurnt;
        
        /// <summary>
        /// Called whenever a card is banned
        /// </summary>
        public event Action<Card> OnCardBanned;
        
        /// <summary>
        /// Called whenever the player's hand hits the max hand size
        /// </summary>
        public event Action<Card> OnMaxHandSizeReached;
        
        /// <summary>
        /// Called whenever the player's field hits the max field size
        /// </summary>
        public event Action<Card> OnMaxFieldSizeReached;

        /// <summary>
        /// This decides if the player is on the first or second round
        /// </summary>
        public PlayerRoundEnum Round;


        public Player(PlayerRoundEnum round)
            : this()
        {
            this.Round = round;
            this.Options = PlayerManager.Instance.CardGameOptions;
        }
        public Player(CardGameOptions options, IEnumerable<Card> choosenDeck)
            : this()
        {
            this.PlayerId = Guid.NewGuid();
            this.ActiveDeck = choosenDeck.ToList();
            this.Options = options;
        }

        private Player()
        {
            this.Hand = new List<Card>();
            this.Field = new List<Card>();
            this.Graveyard = new List<Card>();
            this.Discarded = new List<Card>();
            this.Banned = new List<Card>();
            this.Burned = new List<Card>();
        }

        /// <summary>
        /// This Draw a card from the active deck.
        /// If it hits the max hand size, it will trigger the MaxHandSizeReached event.
        /// You should create your own logic to handle the MaxHandSizeReached event.
        /// </summary>
        /// <param name="card"></param>
        public void DrawCard(Card card)
        {
            this.ActiveDeck.Remove(card);
            if(this.Hand.Count >= this.Options.MaxHandSize)
            {
                this.OnMaxHandSizeReached?.Invoke(card);
                return;
            }
            this.Hand.Add(card);
            this.OnCardDrawn?.Invoke(card);
        }

        /// <summary>
        /// This plays a card from the hand to the field.
        /// If it hits the max field size, it will trigger the MaxFieldSizeReached event.
        /// You should create your own logic to handle the MaxFieldSizeReached event.
        /// </summary>
        /// <param name="card"></param>
        public void PlayCard(Card card)
        {
            this.Hand.Remove(card);
            if(this.Field.Count >= Options.MaxFieldSize)
            {
                this.OnMaxFieldSizeReached?.Invoke(card);
                return;
            }
            this.Field.Add(card);
            this.OnPlayCard?.Invoke(card);
        }

        /// <summary>
        /// This removes a card from the field.
        /// </summary>
        /// <param name="card"></param>
        public void CardLeaveField(Card card)
        {
            this.Field.Remove(card);
            this.OnCardLeftField?.Invoke(card);
        }

        /// <summary>
        /// This discards a card from the hand. And follow the options gave on the CardGameOptionsBuilder
        /// Default is DiscardToDiscardPile
        /// </summary>
        /// <param name="card">The card to be discarded</param>
        public void DiscardCard(Card card)
        {
            this.Hand.Remove(card);
            if(Options.DiscardToGraveyard)
                this.Graveyard.Add(card);
            else if(Options.DiscardToDiscardPile)
                this.Discarded.Add(card);
            this.OnCardLeftHand?.Invoke(card);
            this.OnCardDiscarded?.Invoke(card);
        }
        
        /// <summary>
        /// This adds a card to the banned list.
        /// Obs: this does not remove the card from the field, deck or hand. You should handle your own burn logic
        /// </summary>
        /// <param name="card">The card to be banned</param>
        public void BurnCard(Card card)
        {
            this.Burned.Add(card);
            this.OnCardBurnt?.Invoke(card);
        }

        /// <summary>
        /// This adds a card to the banned list.
        /// 
        /// </summary>
        /// <param name="card"></param>
        public void BanCard(Card card)
        {
            this.Banned.Add(card);
            this.OnCardBanned?.Invoke(card);
        }

        private List<Card> Hand { get; }
        private List<Card> Field { get; }
        private List<Card> ActiveDeck { get; }
        private List<Card> Graveyard { get; }
        private List<Card> Discarded { get; }
        private List<Card> Banned { get; }
        private List<Card> Burned { get; }
        private CardGameOptions Options { get; }
    }
}