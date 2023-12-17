using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerEsquivarMG : MonoBehaviour
{
    [SerializeField] int LineToAct;
    public Dialogues dialogues;
    [SerializeField] EsquivarMG Mg;

    private void Update()
    {
        if (dialogues.lineIndex == LineToAct) //Quan la linea del dialeg coincideix amb LineToGive dona l item
        {
            Mg.enabled= true;
            this.enabled = false;
        }
    }
}
