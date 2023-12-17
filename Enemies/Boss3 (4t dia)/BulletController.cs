using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float BulletXSpeed;
    public float BulletDamage;
    [SerializeField] bool isFacingRight;
    [SerializeField] float BulletYOffsetSpawn;
    [SerializeField] float SinusAmplitude;
    [SerializeField] float SinusAltitude;

    void Update()
    {
        float _x;
        if (isFacingRight)
        {
            _x = gameObject.transform.position.x - BulletXSpeed;
        }
        else
        {
            _x = gameObject.transform.position.x + BulletXSpeed;
        }
        float _y = (SinusAltitude*PositionFunction1(_x))-BulletYOffsetSpawn;
        gameObject.transform.position = new Vector3(_x, _y, 0f);
    }

    private float PositionFunction1(float _x)
    {
        return (math.sin(_x/SinusAmplitude));
    }

    public void DefineBullet()
    {
        Transform PlayerPos = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        if (transform.position.x > PlayerPos.transform.position.x)
        {
            isFacingRight= true;
        }
        else
        {
            isFacingRight= false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide" + collision.name);
        if (collision.CompareTag("Paret"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.PlayerGetDamage(BulletDamage);
            Destroy(gameObject);
        }
    }
}
