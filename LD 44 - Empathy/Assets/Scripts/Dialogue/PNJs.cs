using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector), typeof(DialogueManager), typeof(BoxCollider2D))]
public class PNJs : MonoBehaviour
{
    public int ID;

    GameObject flamme, E;
    DialogueManager dm;
    PlayableDirector dir; //Pour jouer le flash

    [HideInInspector] public bool hasInteractionStarted = false;
    [HideInInspector] public bool hasInteractionEnded = false;
    [HideInInspector] public bool isFlameVisible = false;

    void Start()
    {
        flamme = transform.GetChild(1).gameObject;
        E = transform.GetChild(0).gameObject;

        flamme.SetActive(false);
        E.SetActive(false);

        dir = GetComponent<PlayableDirector>();
        dm = GetComponent<DialogueManager>();

        dm.dialogues[0].onDialogueEnded += ShowFlamme;

        dir.playOnAwake = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void ShowFlamme()
    {
        flamme.SetActive(true);
        isFlameVisible = true;

    }



    private void OnTriggerStay2D(Collider2D c)
    {
        if (!hasInteractionEnded && isFlameVisible)
        {
            E.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightShift))
            {
                Counter.instance.Beep();
                EndInteraction();
            }
        }
    }

    private void EndInteraction()
    {
        hasInteractionEnded = true;
        E.SetActive(false);

        ActivateFlash();
        RemovePoint();
        TriggerDialogue(1);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        E.SetActive(false);

    }







    public void TriggerDialogue(int index)
    {
        hasInteractionStarted = true;
        dm.ReadDialogue(index);
    }

    public void ActivateFlash()
    {
        dir.Play();
    }

    public void RemovePoint()
    {
        Counter.instance.AddPoints(ID);
    }

}
