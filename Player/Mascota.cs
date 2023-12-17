using UnityEngine;

public class Mascota : MonoBehaviour
{
    public Transform PlayerTransform;
    [SerializeField] float DistanceFromPlayer;
    [SerializeField] float MovementSpeed;


    void Start()
    {
        PlayerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        
        if (transform.position.x > PlayerTransform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (Vector2.Distance(transform.position, PlayerTransform.position) > DistanceFromPlayer)
            {
                transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            if (Vector2.Distance(transform.position, PlayerTransform.position) > DistanceFromPlayer)
            {
                transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime);
            }
        }
    }
    private void OnEnable()
    {
        transform.position = new Vector2(PlayerTransform.position.x - 2f, -3.34f);
    }
}
