using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBallEnemyAttack : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed = 10; 

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed); 

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
