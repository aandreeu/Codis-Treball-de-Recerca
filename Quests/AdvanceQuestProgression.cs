using UnityEngine;

public class AdvanceQuestProgression : MonoBehaviour
{
    [SerializeField] bool IsPlayerInRange;
    public string QuestTitleToCheck;
    public int QuestProgressionToCheck;
    public QuestPlayerHandler questHandler;
    public enum TypeOfQuestPhase
    {
        GoingToAPlace,
        InteractWith
    }
    [SerializeField] TypeOfQuestPhase typeOfQuest;

    private void Start()
    {
        questHandler=FindObjectOfType<QuestPlayerHandler>();
        IsPlayerInRange = false;
    }

    private void Update()
    {
        if (IsPlayerInRange)
        {
            if (Input.GetKey("e"))
            {
                AdvanceMission();
            }
        }
    }

    public void AdvanceMission() //Avancar en un el progres de la missio i desactivar el script pq no es pugui activar mes dun cop
    {
        if (questHandler.quest.QuestPorgression+1 == questHandler.quest.QuestPhases.Length && questHandler.quest.QuestTitle == QuestTitleToCheck && questHandler.quest.QuestPorgression == QuestProgressionToCheck) //Acabar la missio si s arriba al final
        {
            Debug.Log("Acaba la missio " + QuestTitleToCheck + " GameObject:" + gameObject.name);
            questHandler.quest.QuestGoal.IsReached = true;
            var playerInventory = FindObjectOfType<PlayerInventory>();
            playerInventory.ManipulateMoney(questHandler.quest.MoneyReward);
            this.enabled = false;
            this.hideFlags = HideFlags.HideInInspector;
        }
        else if (questHandler.quest.QuestTitle == QuestTitleToCheck && questHandler.quest.QuestPorgression==QuestProgressionToCheck)
        {
            Debug.Log("Avanca la missio " + QuestTitleToCheck + " GameObject:" + gameObject.name);
            questHandler.quest.QuestPorgression++;
            this.enabled = false;
            this.hideFlags = HideFlags.HideInInspector;
        }
        else
        {
            Debug.Log("res");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si el tipus de fase es d anar a algun lloc, al topar amb aquesta barrera invisible cridem a advanceMission
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (typeOfQuest == TypeOfQuestPhase.GoingToAPlace && this.enabled == true)
            {
                AdvanceMission();
            }
            IsPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }
}
