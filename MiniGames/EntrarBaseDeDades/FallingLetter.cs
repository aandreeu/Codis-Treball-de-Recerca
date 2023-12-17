using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FallingLetter : MonoBehaviour
{
    public float FallingSpeed;
    public int LetterId;
    public TMP_Text LetterDisplay;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Rigidbody2D LetterRigidbody;

    [SerializeField] string[] Eng_Abecedari = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
    "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

    private void OnEnable()
    {
        if (gameObject.GetComponentInParent<LetterSpawner>().WhichSpawner == 0)
        {
            LetterId = Random.Range(0, Eng_Abecedari.Length - 20);
        }
        else if (gameObject.GetComponentInParent<LetterSpawner>().WhichSpawner == 1)
        {
            LetterId = Random.Range(0, Eng_Abecedari.Length - 18);
        }
        else if (gameObject.GetComponentInParent<LetterSpawner>().WhichSpawner == 2)
        {
            LetterId = Random.Range(3, Eng_Abecedari.Length - 13);
        }
        LetterDisplay.text = Eng_Abecedari[LetterId];
        LetterRigidbody= gameObject.GetComponent<Rigidbody2D>();
        LetterRigidbody.gravityScale = FallingSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LetterDestroyer"))
        {
            Destroy(gameObject);
        }
    }


}
