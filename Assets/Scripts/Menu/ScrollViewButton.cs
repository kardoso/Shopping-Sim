using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewButton : MonoBehaviour
{
    public ScrollRect rect;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            rect.FocusOnItem(this.GetComponent<RectTransform>());
        }
    }
}
