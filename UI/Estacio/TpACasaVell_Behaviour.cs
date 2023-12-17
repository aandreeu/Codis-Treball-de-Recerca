using UnityEngine;
using UnityEngine.UI;

public class TpACasaVell_Behaviour : MonoBehaviour
{

    [SerializeField] string QuestTitleToCompare;
    [SerializeField] int QuestProgressionToCompare;
    public Button boto;

    private void OnEnable()
    {
        QuestPlayerHandler questPlayerHandler = FindObjectOfType<QuestPlayerHandler>();
        if (questPlayerHandler.quest.QuestTitle==QuestTitleToCompare && questPlayerHandler.quest.QuestPorgression==QuestProgressionToCompare)
        {
            boto.interactable = true;
        }
        else
        {
            boto.interactable = false;
        }
    }
}
