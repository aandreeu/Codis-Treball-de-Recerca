using UnityEngine;

public class TriggerMinigame : MonoBehaviour
{
    public MinijocAtzar1 minijocAtzar;
    public PlayerController playerController;
    public GameObject GamePanel;
    [SerializeField] bool IsPlayerInRange;
    public bool CanPlay;

    private void Start()
    {
        playerController=FindObjectOfType<PlayerController>();
        CanPlay=true;
    }
    private void Update()
    {
        if(IsPlayerInRange && Input.GetKeyDown("e")&&CanPlay)
        {
            GamePanel.SetActive(true);
            minijocAtzar.StartGame();
            CanPlay= false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
