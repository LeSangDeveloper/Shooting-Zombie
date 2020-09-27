using UnityEngine;

public class GetDamage : MonoBehaviour
{
    int dame = 1;

    Transform fullBody;

    void Awake()
    {
        fullBody = transform.parent;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            EnemyMover temp = fullBody.GetComponent<EnemyMover>();
        
            if (temp != null)
            {
                temp.subtractHealth(dame);
            }
        }
    }

}