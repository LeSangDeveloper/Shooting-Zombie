using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMover : MonoBehaviour
{

    public Transform player;
    // Reference to the player's position.

    Animator zombieAnimation;

    NavMeshAgent nav;
    // Reference to the nav mesh agent.

    [Header("Attribute")]
    public float speed;

    void Awake ()
    {
        if (GameObject.FindGameObjectWithTag ("Player").transform != null)
            // Set up the references.
            player = GameObject.FindGameObjectWithTag ("Player").transform; 

        // Do not use FindGameObjectWithTag() method if the scene hierarchy is too big. 
        // In such case, declare 'player' as a public variable & set reference from the inspector.
    
        zombieAnimation = GetComponent<Animator>();

        nav = GetComponent <NavMeshAgent> ();
    }

    void Update ()
    {
        if (player != null)
        {
            if ((this.transform.position - player.position).sqrMagnitude < 3f)
            {
                nav.speed = 0;
            }
            else
            {
                nav.speed = this.speed;
            }
        }
        // Set the destination of the nav mesh agent to the player.
        if (player != null)
            nav.SetDestination (player.position);

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

    public void subtractHealth(int dame)
    {
        nav.enabled = false;
        StartCoroutine(animationDead());
    }

}
