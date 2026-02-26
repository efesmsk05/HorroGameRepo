using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyFollow : MonoBehaviour
{
    public static enemyFollow instance;
    public LayerMask groundLayer, playerLayer;
    public NavMeshAgent agent;
    public float speed;
    public float followSpeed;

    //Devriye
    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    float defaultWalkPointRange;
    float enemyWaitTime = 0f;
    //Takip
    public float minDistance; //Düþmanýn ne kadar yakýnlaþacaðý
    public float sightRange; //Playerý algýladýðý alan
    bool inSightRange;
    bool isAlarmed = false;
    bool isAlarmed2 = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        defaultWalkPointRange = walkPointRange;
        agent.speed = speed;
       
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    void Update()
    {
        inSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer); //sightRange çapýnda bir alaný tarayýp player objesi içinde mi kontrol ediyor

        if (!inSightRange)
        {
            Patroling();
        }

        if (inSightRange)
        {
            if ((playerController.instance.isRunning || playerController.instance.isWalking) && !playerController.instance.isCrouching) //player yürüyor ya da koþuyorsa
            {
                if (Vector3.Distance(transform.position, playerController.instance.GetComponent<Transform>().position) > minDistance) //enemy player'a minDistance'dan daha uzaksa
                {
                    FollowPlayer();
                }
                else
                {
                    Patroling();
                }
            }
            else
            {
                Patroling();
            }
        }
        
    }

    private void Patroling() //Devriye
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        if (enemyWaitTime > 0)
        {
            enemyWaitTime -= Time.deltaTime;
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) //gideceði noktaya vardýysa
        {
            if (isAlarmed)
            {
                agent.speed = speed;
                StartCoroutine(alarm(5f));
                isAlarmed = false;
            }
            if (enemyWaitTime <= 0)
            {
                walkPointSet = false;
                if (isAlarmed2)
                {
                    enemyWaitTime = 0f;
                }
            }
        }
    }

    private void SearchWalkPoint() //random walk point belirleme
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
            
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) //walk point zemin içerisindeyse
        {
            walkPointSet = true;
        }
    }

    private void FollowPlayer()
    {
        agent.speed = followSpeed;
        if (isAlarmed2)
        {
            isAlarmed2 = false;
        }
        if (Physics.Raycast(playerController.instance.GetComponent<Transform>().position, -transform.up, 2f, groundLayer))
        {
            walkPoint = new Vector3(playerController.instance.GetComponent<Transform>().position.x, playerController.instance.GetComponent<Transform>().position.y, playerController.instance.GetComponent<Transform>().position.z);
            if (Physics.Raycast(walkPoint, -transform.up, 5f, groundLayer)) //walk point zemin içerisindeyse
            {
                walkPointSet = true;
                isAlarmed = true;
            }
        }
        agent.SetDestination(playerController.instance.GetComponent<Transform>().position);
    }

    IEnumerator alarm(float x)
    {
        walkPointRange = 3;
        enemyWaitTime = 0f;
        isAlarmed2 = true;

        yield return new WaitForSecondsRealtime(x);
        if (isAlarmed2)
        {
            walkPointRange = defaultWalkPointRange;
            enemyWaitTime = 0f;
            isAlarmed2 = false;
        }
    }
    
}
