using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapMagatzem_Behaviour : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] CapsuleCollider2D capsuleCollider2D;
    [SerializeField] Dialogues DialegsPorta;
    public void MostrarCapMagatzem()
    {
        boxCollider.enabled = true;
        capsuleCollider2D.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder =4;
        Destroy(DialegsPorta);
    }
}
