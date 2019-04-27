using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavenEnding : MonoBehaviour
{
    public Dialogue dialogueEveryone;
    public Text pnjsText;
    public float startDelay = 3f, delayBetweenApparitions = .75f, apparitionSpeed = 2f;
    public SpriteRenderer[] pnjs;
    public AnimationCurve fadeCurve;



    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        for (int i = 0; i < pnjs.Length; i++)
        {
            pnjs[i].color = new Color(pnjs[i].color.r, pnjs[i].color.g, pnjs[i].color.b, 0f);
        }


        pnjsText.text = "";
        pnjsText.gameObject.SetActive(false);

        yield return new WaitForSeconds(startDelay);

        DialogueManager dm = FindObjectOfType<DialogueManager>();
        dm.ReadDialogue(0);
        dm.dialogues[0].onDialogueEnded += OnFirstDialogueEnded;

        if (PlayerPrefs.GetInt("Everyone") == 1)
        {
            dm.dialogues[1] = dialogueEveryone;
        }

    }

    private void OnFirstDialogueEnded()
    {
        StartCoroutine(SpawnPNJs());

    }

    private IEnumerator SpawnPNJs()
    {

        for (int i = 0; i < pnjs.Length; i++)
        {
            StartCoroutine(SpawnPNJ(i));
            yield return new WaitForSeconds(delayBetweenApparitions);
        }
        
        DialogueManager dm = FindObjectOfType<DialogueManager>();
        dm.ReadDialogue(1, pnjsText);
        dm.dialogues[1].onDialogueEnded += OnSecondDialogueEnded;
    }


    private IEnumerator SpawnPNJ(int i)
    {
        float t = 0f;

        if (PlayerPrefs.GetInt($"Has_PNJ_{i}_Been_Saved") == 1)
        {
            while (t < 1f)
            {
                t -= Time.unscaledDeltaTime * apparitionSpeed;
                float a = fadeCurve.Evaluate(t);
                Color newCol = pnjs[i].color;
                pnjs[i].color = new Color(newCol.r, newCol.g, newCol.b, a);
                yield return 0;
            }
        }

    }

    private void OnSecondDialogueEnded()
    {
        SceneFader.instance.FadeToScene(PlayerPrefs.GetInt("Everyone") == 1 ? 3 : 0);
    }
}

