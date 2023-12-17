using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class AmpollaProjectil : MonoBehaviour
{
    public float BulletSpeed;
    public float BulletDamage;
    [SerializeField] float BulletYOffsetSpawn;
    [SerializeField] float AngleAlpha;
    [SerializeField] float timer;
    [SerializeField] int BulletPattern;

    void Update()
    {
        float _x;
        float _y;
        if (BulletPattern == 1)
        {
            _x = gameObject.transform.position.x + BulletSpeed * math.cos(AngleAlpha);
            _y = gameObject.transform.position.y + BulletSpeed * math.sin(AngleAlpha);
            gameObject.transform.position = new Vector3(_x, _y, 0f);
        }
    }
    public void DefineBullet(int _pattern, float alpha)
    {
        BulletPattern = _pattern;
        AngleAlpha = alpha * (math.PI / 180);
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
