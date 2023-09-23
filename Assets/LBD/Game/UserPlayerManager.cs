using LBD.Managers;

namespace LBD.Game
{
    public class UserPlayerManager : PlayerManager
    {
        public UserPlayerManager() : base(x => x.DiscardToDiscardPile()
        .DrawOnTurnStart()
        .MaxHandSize(10)
        .MaxFieldSize(5)) { }
        protected override void ConfigureNetwork()
        {
            this.Player = NetworkManager.Instance.GetCurrentPlayer();
        }
    }
}