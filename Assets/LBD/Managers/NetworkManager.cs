using LBD.Classes;
using LBD.Enums;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace LBD.Managers
{
    public class NetworkManager
    {
        public static NetworkManager Instance;
        private Player[] _players = new Player[2];
        internal event Action OnLoaded;
        public NetworkManager()
        {
            Instance = this;
            _players[0] = new Player(PlayerRoundEnum.Odds);
            _players[1] = new Player(PlayerRoundEnum.Evens);
            EnemyHasPassed = true;
            Task.Run(CheckPlayers);
        }

        private async Task CheckPlayers()
        {
            while(PlayerManager.Instance is null)
            {
                await Task.Delay(10);
            }
            PlayerManager.Instance.OnPlayerPassed += HandlePlayerPassed;
        }

        private void HandlePlayerPassed()
        {
            this.EnemyHasPassed = !this.EnemyHasPassed;
        }

        public bool EnemyHasPassed;
        

        public Player GetCurrentPlayer() => _players[0];

        public Player GetEnemyPlayer() => _players[1];
    }
}