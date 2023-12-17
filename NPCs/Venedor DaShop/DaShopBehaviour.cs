using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaShopBehaviour : MonoBehaviour
{
    [SerializeField] Dialogues[] dialogues;
    [SerializeField] SetActiveThroughDialogue[] setActives;


    void Start()
    {
        dialogues[0].enabled = true;
        dialogues[1].enabled = false;
        setActives[0].enabled = true;
        setActives[1].enabled = false;
    }

    void Update()
    {
        if (dialogues[0].lineIndex == 6)
        {
            dialogues[0].enabled = false;
            dialogues[1].enabled = true;
            setActives[0].enabled = false;
            setActives[1].enabled = true;
        }
    }
}
