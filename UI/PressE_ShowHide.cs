using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressE_ShowHide : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    public bool CanBeShown;
    public Animator Animator_PressE;
    [SerializeField] GameObject pressE;
    [SerializeField] BoxCollider2D RangeCollider;

    private void Start()
    {
        if (Animator_PressE==null)
        {
            Animator_PressE = gameObject.GetComponent<Animator>();
        }
        pressE.SetActive(false);
        CanBeShown= true;
    }

    private void OnTriggerEnter2D(Collider2D collision)  // Mostra E
    {
        if(CanBeShown)
        {
            pressE.SetActive(true);
            Animator_PressE.SetBool("HasToLoop", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //Amaga E
    {
        Animator_PressE.SetBool("HasToLoop", false);
    }

    public void SetActive(bool active)
    {
        SetActive(active);
    }
}
