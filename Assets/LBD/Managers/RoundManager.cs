using System;
using System.Collections;
using System.Collections.Generic;
using LBD.Classes;
using UnityEngine;

namespace LBD.Managers
{
    public class RoundManager
    {
        public static RoundManager Instance;

        public RoundManager()
        {
            RoundNumber = 0;
            Instance = this;
        }
        public int RoundNumber { get; private set; }
        public event Action OnRoundEnd;
        public event Action OnRoundStart;
        public virtual void EndRound()
        {
            OnRoundEnd?.Invoke();
            this.StartRound();
        }
        public virtual void StartRound()
        {
            RoundNumber++;
            OnRoundStart?.Invoke();
        }
    }
}
