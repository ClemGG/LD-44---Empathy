using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class HellEnding : MonoBehaviour
{

    public float startDelay = 2f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);

        DialogueManager dm = FindObjectOfType<DialogueManager>();
        dm.ReadDialogue(0);
        dm.dialogues[0].onDialogueEnded += OnHellDialogueEnded;
    }

    private void OnHellDialogueEnded()
    {
        GetComponent<PlayableDirector>().Play();
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds((float)GetComponent<PlayableDirector>().playableAsset.duration);
        SceneFader.instance.FadeToScene(0);
    }
}
