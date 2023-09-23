using LBD.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class SquareClickable : MonoBehaviour, IClickable
    {
        public void Click()
        {
            PlayerManager.Instance.Pass();
        }
    }
}
