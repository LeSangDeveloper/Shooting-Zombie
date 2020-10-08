using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMover : MonoBehaviour
{

    // Để enemy không dí theo lúc nằm xuống
    bool isDead = false;
    bool lockAttack = false;
    public Transform PlayerPosition;
    // Reference to the player's position.

    public GameObject PlayerObject;

    Coroutine cr_Attack;

    Animator zombieAnimation;

    NavMeshAgent nav;
    // Reference to the nav mesh agent.

    [Header("Attribute")]
    public float speed;

    void Awake ()
    {
        if (GameObject.FindGameObjectWithTag ("Player").transform != null)
        {
            // Set up the references.
            PlayerPosition = GameObject.FindGameObjectWithTag ("Player").transform; 
            PlayerObject  =PlayerPosition.parent.gameObject;
        }
        // Do not use FindGameObjectWithTag() method if the scene hierarchy is too big. 
        // In such case, declare 'player' as a public variable & set reference from the inspector.
    
        zombieAnimation = GetComponent<Animator>();

        nav = GetComponent <NavMeshAgent> ();
    }

    IEnumerator CR_Attack()
    {
        PlayerHealth playerHealth = null;
        if (PlayerObject != null)
        {
            Debug.Log("test player");
            playerHealth = PlayerObject.gameObject.GetComponent<PlayerHealth>();
        }
        while (true)
        {
            yield return new WaitForSeconds(1.0f);        
            if (playerHealth != null)
                playerHealth.SubtractHealth(5);
            else
            {
                Debug.Log("test");
                playerHealth = PlayerObject.gameObject.GetComponent<PlayerHealth>();
            }
        }
    }

    void Update ()
    {
        if (PlayerPosition != null)
        {
            if ((this.transform.position - PlayerPosition.transform.position).sqrMagnitude < 3f)
            {
                if (!lockAttack)
                {
                    cr_Attack = StartCoroutine(CR_Attack());
                    lockAttack = true;
                }
                nav.speed = 0;
                zombieAnimation.SetBool("In Attack", true);
            }
            else
            {
                if (cr_Attack != null)
                {
                    lockAttack = false;
                    StopCoroutine(cr_Attack);
                    cr_Attack = null;
                }
                nav.speed = this.speed;
                zombieAnimation.SetBool("In Attack", false);
            }
        }

        // Set the destination of the nav mesh agent to the player.
        if (PlayerPosition != null && !isDead)
            nav.SetDestination (PlayerPosition.transform.position);

    }

    void RandomDeadAnimation()
    {
        System.Random random = new System.Random();
        int i = random.Next(1, 4);
        switch (i)
        {
            case 1:
                zombieAnimation.SetBool("Is Dead Right", true);
                break;
            case 2:
                zombieAnimation.SetBool("Is Dead Back", true);        
                break;
            case 3:
                zombieAnimation.SetBool("Is Dead Left", true);
                break;
        }
    }

    IEnumerator animationDead()
    {
        RandomDeadAnimation();
        int i = 0;
        
        while (true)
        {
            i++;
            yield return new WaitForSeconds(2.0f);
            if (i == 10) break;
        }
        
        Destroy(gameObject);
        yield return null;
    }

    public void Dead()
    {
        isDead = true;
        nav.enabled = false;
        StartCoroutine(animationDead());
    }

}
