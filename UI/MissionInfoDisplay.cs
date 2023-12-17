using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionInfoDisplay : MonoBehaviour
{
    public QuestPlayerHandler questPlayer;

    public GameObject QuestGeneralPanel;
    public TMP_Text TitleQuest;
    public TMP_Text DescriptionQuest;
    public TMP_Text RewardQuest;
    public TMP_Text StatusQuest;
    [SerializeField] string CompletedQuest;
    [SerializeField] string UncompletedQuest;
    public TMP_Text ProgressionQuest;

    public void ShowQuestInfo()  //Escriure en el canvas la descripcio de la missio
    {
        QuestGeneralPanel.SetActive(true);
        TitleQuest.text = questPlayer.quest.QuestTitle;
        DescriptionQuest.text = questPlayer.quest.QuestDescription;
        RewardQuest.text = questPlayer.quest.MoneyReward.ToString();
        if(questPlayer.quest.QuestGoal.IsReached==true)
        {
            StatusQuest.text = CompletedQuest;
        }
        else
        {
            StatusQuest.text = UncompletedQuest;
        }
        ProgressionQuest.text = questPlayer.quest.QuestPhases[questPlayer.quest.QuestPorgression]; //Diu que ha de fer despres segons el progres de la missio
    }
}
