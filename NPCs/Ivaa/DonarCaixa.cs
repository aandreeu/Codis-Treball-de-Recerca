using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonarCaixa : MonoBehaviour
{
    [SerializeField] GameObject CaixaFlotant;
    [SerializeField] Dialogues dialogues;
    [SerializeField] int dialogueIndexToCheck;
    private void OnEnable()
    {
        CaixaFlotant= FindObjectOfType<Mascota>().gameObject;
    }
    void Update()
    {
        if(dialogues.lineIndex==dialogueIndexToCheck)
        {
            CaixaFlotant.GetComponent<SpriteRenderer>().enabled=true;
            Destroy(this);
        }
    }
}
