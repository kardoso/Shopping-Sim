using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanelControls : MonoBehaviour
{
    Image image;
    [SerializeField] GameObject controlsPanel;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSecondsRealtime(3);

        var imageColor = image.color;

        while (true)
        {
            yield return new WaitForSecondsRealtime(0.05f);

            imageColor.a += 0.1f;
            image.color = imageColor;

            if(imageColor.a >= 1)
            {
                break;
            }
        }

        controlsPanel.SetActive(false);

        while (true)
        {
            yield return new WaitForSecondsRealtime(0.05f);

            imageColor.a -= 0.1f;
            image.color = imageColor;

            if (imageColor.a <= 0)
            {
                break;
            }
        }

        gameObject.SetActive(false);
    }
}
