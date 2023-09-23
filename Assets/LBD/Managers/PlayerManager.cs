using System;
using System.Threading.Tasks;
using LBD.Classes;
using LBD.Enums;
using UnityEngine;

namespace LBD.Managers
{
    public abstract class PlayerManager
    {
        public static PlayerManager Instance;
        public bool PlayerRound;
        internal event Action OnLoaded;

        public Player Player { get; protected set; }
        public event Action<ActionSpeedEnum> OnActionPrepared;
        public event Action<ActionSpeedEnum> OnActionPerformed;
        public event Action OnPlayerPassed;
        public CardGameOptions CardGameOptions { get; protected set; } = new CardGameOptions();
        
        public PlayerManager(Action<CardGameOptionsBuilder> configureBuild) : this()
        {
            var builder = new CardGameOptionsBuilder();
            configureBuild(builder);
            this.CardGameOptions = builder.Build();
        }

        public PlayerManager(CardGameOptions options) : this()
        {
            this.CardGameOptions = options;
        }
        
        private PlayerManager()
        {
            RoundManager.Instance.OnRoundStart += HandleRoundStart;
            RoundManager.Instance.OnRoundEnd += HandleRoundEnd;
            Instance = this;
            OnLoaded?.Invoke();
            Task.Run(CheckNetwork);
        }

        private async Task CheckNetwork()
        {
            while(NetworkManager.Instance is null)
            {
                await Task.Delay(10);
            }
            ConfigureNetwork();
        }

        protected abstract void ConfigureNetwork();

        private void HandleRoundEnd() => PlayerRound = false;
        private void HandleRoundStart() => SetPlayerTurn();

        public void Pass()
        {
            if(NetworkManager.Instance.EnemyHasPassed)
            {
                RoundManager.Instance.EndRound();
            }
            OnPlayerPassed?.Invoke();
        }
        private void SetPlayerTurn()
        {
            this.PlayerRound = this.Player.Round switch
            {
                PlayerRoundEnum.Evens => RoundManager.Instance.RoundNumber % 2 == 0,
                PlayerRoundEnum.Odds => RoundManager.Instance.RoundNumber % 2 != 0,
                _ => throw new NotImplementedException(),
            };
        }
    }
}