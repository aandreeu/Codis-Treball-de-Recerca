using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaAbusadors_Managers : MonoBehaviour
{
    [Header("Abusadors")]
    [SerializeField] GameObject[] Abusadors;
    [SerializeField] GameObject Abusat;
    [SerializeField] float TimeAtacs;
    [SerializeField] bool PotContar;
    [Header("Player")]
    [SerializeField] QuestPlayerHandler questPlayerHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Abusadors[0].GetComponent<Abusador_Behaviour>().enabled = true;
            Abusadors[1].GetComponent<Abusador_Behaviour>().enabled = true;
            if (questPlayerHandler.quest.QuestTitle == "Quart dia de feina." && questPlayerHandler.quest.QuestPorgression == 9)
            {
                questPlayerHandler.quest.QuestPorgression++;
                PotContar = false;
                Abusadors[0].GetComponent<Abusador_Behaviour>().DetectingRadius = 12f;
                Abusadors[1].GetComponent<Abusador_Behaviour>().DetectingRadius = 12f;
                SceneLoaderForPlayer[] tps = FindObjectsOfType<SceneLoaderForPlayer>();
                foreach (var tp in tps)
                {
                    tp.CanPlayerTp= false;
                }
            }
        }
    }

    private void Update()
    {
        if (PotContar)
        {
            TimeAtacs += Time.deltaTime;
            if (TimeAtacs > 1.5f)
            {
                int i = Random.Range(0, 2);
                if(i==0)
                {
                    Abusadors[0].GetComponent<Animator>().Play("PatadaTerra_Abusador");
                    Abusadors[1].GetComponent<Animator>().Play("PegarTerra_Abusador");
                }
                else
                {
                    Abusadors[1].GetComponent<Animator>().Play("PatadaTerra_Abusador");
                    Abusadors[0].GetComponent<Animator>().Play("PegarTerra_Abusador");
                }
                TimeAtacs = 0f;
            }
        }
    }

    private void OnEnable()
    {
        questPlayerHandler=FindObjectOfType<QuestPlayerHandler>();
        if (questPlayerHandler.quest.QuestTitle=="Quart dia de feina." && questPlayerHandler.quest.QuestPorgression == 9)
        {
            Abusadors[0].SetActive(true);
            Abusadors[1].SetActive(true);
            Abusat.SetActive(true);
            PotContar = true;
        }
        else
        {
            Abusadors[0].SetActive(false);
            Abusadors[1].SetActive(false);
            Abusat.SetActive(false);
        }
    }

}
