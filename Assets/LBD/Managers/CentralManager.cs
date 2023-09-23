using System;
using UnityEngine;
using LBD.Enums;
using LBD.Game;
using System.Threading.Tasks;

namespace LBD.Managers
{
    public abstract class CentralManager : MonoBehaviour
    {
        public static CentralManager Instance;
        private bool ServicesLoaded = false;

        protected void Awake()
        {
            Instance = this;
            ConfigureServices();
            Task.Run(CheckForServices);
        }

        public abstract void ConfigureServices();

        private async Task CheckForServices()
        {
            while(RoundManager.Instance is null || PlayerManager.Instance is null || NetworkManager.Instance is null)
            {
                await Task.Delay(10);
            }
            ServicesLoaded = true;
            OnServicesLoaded();
        }

        protected abstract void OnServicesLoaded();

        public virtual void RaiseParameterlessEvent(ParameterLessEvent @event)
        {
            switch (@event)
            {
                case ParameterLessEvent.OnRoundEnd:
                    RoundManager.Instance.EndRound();
                    break;
                case ParameterLessEvent.OnRoundStart:
                    RoundManager.Instance.StartRound();
                    break;
                case ParameterLessEvent.OnPlayerPassed:
                    PlayerManager.Instance.Pass();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@event), @event, string.Empty);
            }
        }

        private void Update()
        {
            if (ServicesLoaded)
                ServiceUpdate();
        }

        protected virtual void ServiceUpdate() { }
    }
}