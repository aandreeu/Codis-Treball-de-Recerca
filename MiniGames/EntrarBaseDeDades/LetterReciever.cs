using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterReciever : MonoBehaviour
{
    public int RecievedLetterID;
    [SerializeField] LetterMGManager letterManager;
    [SerializeField] int NumberToPress;
    [SerializeField] bool IsLetterInRange;
    [SerializeField] bool HasRecievedLetter;
    [SerializeField] Image RecieverDisplay;
    [SerializeField] Sprite NonPressedAlpha;
    [SerializeField] Sprite PressedAlpha;
    [SerializeField] LetterSpawner letterSpawner;

    private void Start()
    {
        HasRecievedLetter= false;
    }

    private void Update()
    {
        if (Input.GetKeyDown((NumberToPress).ToString()))
        {
            if (IsLetterInRange && RecievedLetterID == letterManager.SearchedWord[letterSpawner.WhichSpawner])
            {
                letterManager.RecievedWord[NumberToPress-1] = RecievedLetterID;
                HasRecievedLetter= true;
            }
        }
        else if(HasRecievedLetter)
        {
            RecieverDisplay.color = new Color(0f, 156f, 0f, 156f);
            letterSpawner.enabled = false;
            this.enabled= false;
        }
        
        if(Input.GetKey(NumberToPress.ToString()))
        {
            RecieverDisplay.sprite = PressedAlpha;
        }
        else
        {
            RecieverDisplay.sprite = NonPressedAlpha;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Letter_MG"))
        {
            RecievedLetterID = collision.GetComponent<FallingLetter>().LetterId;
            IsLetterInRange=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Letter_MG"))
        {
            IsLetterInRange=false;
        }
    }
}
