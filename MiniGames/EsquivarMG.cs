using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class EsquivarMG : MonoBehaviour
{      
    [SerializeField] float TimerToStart;
    [SerializeField] float TimerToEnd;
    [SerializeField] TMP_Text TextTimerStart;
    [SerializeField] GameObject TextTimerStart_GO;

    [Header("Atributs Projectil")]
    [SerializeField] PlayableDirector director;
    [SerializeField] bool IsMiniGameStarted;

    void Start()
    {
        TimerToStart = 5f;
        TextTimerStart_GO.SetActive(true);
    }

    void Update()
    {
        if (TimerToStart > 0) {
            TimerToStart -= 1 * Time.deltaTime;
            TextTimerStart.text = TimerToStart.ToString("0");            
        }
        else { TextTimerStart_GO.SetActive(false); }
        
        if(TimerToStart<=0 && !IsMiniGameStarted)
        {
            IsMiniGameStarted = true;
            director.Play();
        }

    }


}



