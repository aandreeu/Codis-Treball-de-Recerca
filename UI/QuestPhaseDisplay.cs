using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPhaseDisplay : MonoBehaviour
{
    public QuestPlayerHandler questHandler;
    [SerializeField] TMP_Text questDisplay;
    private void Start()
    {
        questHandler=FindObjectOfType<QuestPlayerHandler>();
        questDisplay = gameObject.GetComponent<TMP_Text>();
    }
    void Update()
    {
        questDisplay.text = questHandler.quest.QuestPhases[questHandler.quest.QuestPorgression];
    }
}
