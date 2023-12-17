using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image HealthBar;
    [SerializeField] float ActualHealth;
    [SerializeField] float MaxHealth=100;
    [SerializeField] PlayerController playerController;

    void Start()
    {
        HealthBar=GetComponent<Image>();
        playerController=FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        ActualHealth = playerController.ActualHealth;

        HealthBar.fillAmount = ActualHealth / MaxHealth;
    }
}
