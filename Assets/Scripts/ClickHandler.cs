using UnityEngine;

namespace Assets.Scripts
{
    public class ClickHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D obj = Physics2D.OverlapPoint(mousePosition);
                if (obj != null)
                {
                    var clickObj = obj.gameObject.GetComponent<IClickable>();
                    if (clickObj != null)
                    {
                        clickObj.Click();
                    }
                }
            }
        }
    }
}
