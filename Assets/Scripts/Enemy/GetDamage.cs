using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public int dame = 1;

    public ParticleSystem BloodEffect;

    Transform fullBody;

    void Awake()
    {
        fullBody = transform.parent;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            EnemyHealth temp = fullBody.GetComponent<EnemyHealth>();

            BloodEffect.Play();

            if (temp != null)
            {
                temp.SubtractHealth(dame);
            }
        }
    }

}