using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject controlsPanel;

    private void Start()
    {
        controlsPanel.SetActive(false);
    }
    public void PLAY()
    {
        SceneFader.instance.FadeToScene(1);
    }
    public void CONTROLS()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }
    public void QUIT()
    {
        SceneFader.instance.FadeToQuitScene();
    }
}
