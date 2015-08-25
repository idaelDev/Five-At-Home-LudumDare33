using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public CanvasGroup mainCanvas;
    public CanvasGroup howToCanvas;

    public void BackOnClick()
    {
        mainCanvas.alpha = 1;
        mainCanvas.interactable = true;
        mainCanvas.blocksRaycasts = true;
        howToCanvas.alpha = 0;
        howToCanvas.interactable = false;
        howToCanvas.blocksRaycasts = false;
    }

    public void HowToOnClick()
    {
        mainCanvas.alpha = 0;
        mainCanvas.interactable = false;
        mainCanvas.blocksRaycasts = false;
        howToCanvas.alpha = 1;
        howToCanvas.interactable = true;
        howToCanvas.blocksRaycasts = true;
    }

    public void NewGameOnClick()
    {
        Application.LoadLevel("Main");
    }

    public void LoadTitle()
    {
        Application.LoadLevel("Title");
    }
}
