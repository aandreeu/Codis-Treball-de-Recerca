using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetActiveThroughDialogue : MonoBehaviour
{
    [SerializeField] int LineToAct;
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] int gameObjectToHide;
    [SerializeField] Dialogues dialogue;
    [SerializeField] bool TrueFalse;

    private void Start()
    {
        dialogue = gameObject.GetComponent<Dialogues>();
    }
    private void Update() //Quan la linea del dialeg coincideix amb LineToAct obre el que sigui
    {
        if (dialogue.lineIndex == LineToAct)
        {
            gameObjects[gameObjectToHide].SetActive(TrueFalse);
        }
    }

}
