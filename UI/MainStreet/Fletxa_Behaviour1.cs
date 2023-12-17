using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fletxa_Behaviour1 : MonoBehaviour
{
    [SerializeField] string[] QuestTitlesToCompare;
    [SerializeField] int[] QuestProgressionsToCompare;
    [SerializeField] Transform[] Transforms;
    [SerializeField] QuestPlayerHandler questhandler;
    [SerializeField] GameObject Fletxa;
    [SerializeField] float timer;

    private void Start()
    {
        questhandler= FindObjectOfType<QuestPlayerHandler>();
    }
    private void Update()
    {
        if (timer > 0f)
        {
            timer=-Time.deltaTime;
        }
        else
        {
            MoureFletxa();
            timer = 5f;
        }
    }

    private void MoureFletxa()
    {
        if (QuestTitlesToCompare[0] == questhandler.quest.QuestTitle && QuestProgressionsToCompare[0] == questhandler.quest.QuestPorgression)
        {
            Fletxa.SetActive(true);
            Fletxa.transform.position = Transforms[0].position;
        }
        else if (QuestTitlesToCompare[1] == questhandler.quest.QuestTitle && QuestProgressionsToCompare[1] == questhandler.quest.QuestPorgression)
        {
            Fletxa.SetActive(true);
            Fletxa.transform.position = Transforms[1].position;
        }
        else
        {
            Fletxa.SetActive(false);
        }
    }
}
