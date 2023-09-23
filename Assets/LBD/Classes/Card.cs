using System;

namespace LBD.Classes
{
    public abstract class Card
    {
        private Player _player;
        public event Action OnPlay;
        public event Action OnDraw;
        public event Action OnDiscard;
        public event Action OnBurn;
        protected Card()
        {
            OnPlay += () => this._player.PlayCard(this);
            OnDraw += () => this._player.DrawCard(this);
        }
    }
}
