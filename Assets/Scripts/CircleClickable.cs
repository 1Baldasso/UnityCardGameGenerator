using LBD.Game;
using LBD.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class CircleClickable : MonoBehaviour, IClickable
    {
        public void Click()
        {
            Debug.Log(RoundManager.Instance.RoundNumber.ToString());
        }
    }
}
