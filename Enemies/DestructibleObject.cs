using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObject : Enemy
{

    private void Update()
    {
        if(ActualHealth<=0)
        {
            Destroy(gameObject);
        }
    }

}
