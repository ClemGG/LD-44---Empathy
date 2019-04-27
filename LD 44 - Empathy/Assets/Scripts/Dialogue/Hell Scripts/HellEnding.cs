using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //A FAIRE : Ajouter l'animation du personnage qui se fait dévorer par sa flamme
        SceneFader.instance.FadeToScene(0);
    }
}
