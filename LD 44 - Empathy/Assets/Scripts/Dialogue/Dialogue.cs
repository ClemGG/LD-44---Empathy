using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "New Dialogue")]
public class Dialogue : ScriptableObject
{

    public Replique[] répliques;
    [HideInInspector] public int index = 0;

    public delegate void OnDialogueEnded();
    public OnDialogueEnded onDialogueEnded;
    


    public string StartDialogue()
    {
        index = 0;
        return répliques[index++].repliqueText;
    }

    public string NextReplique()
    {
        if(index < répliques.Length)
        {
            return répliques[index++].repliqueText;
        }
        else
        {
            onDialogueEnded?.Invoke();
            return null;
        }
    }



}
