using UnityEngine;
using GridBlast.GridSystem.Nodes;

namespace GridBlast.InputSystem
{
    public class InputController : MonoBehaviour
    {
        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if(Input.GetMouseButtonDown(0))
            {
                CastRayToScreen(Input.mousePosition);
            }
#elif UNITY_IOS || UNITY_ANDROID
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    CastRayToScreen(touch.position);
                }
            }
#endif
        }

        private void CastRayToScreen(Vector2 inputPosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(inputPosition), Vector2.zero);
            if(hit.collider != null)
            {
                if(hit.transform.TryGetComponent(out IClickable clickable))
                {
                    clickable.Click();
                }
            }
        }
    }
}