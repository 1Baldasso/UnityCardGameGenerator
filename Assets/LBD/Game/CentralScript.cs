using LBD.Game;
using LBD.Managers;
using UnityEngine;

public class CentralScript : CentralManager
{
    public override void ConfigureServices()
    {
        new RoundManager();
        new UserPlayerManager();
        new NetworkManager();
    }

    protected override void OnServicesLoaded()
    {
        PlayerManager.Instance.OnPlayerPassed += () => Debug.Log($"Player Passed | IsPlayerTurn?{PlayerManager.Instance.PlayerRound}");
    }

    protected override void ServiceUpdate()
    {
       
    }
}
