using UnityEngine;
using UnityEngine.AI;

public class VeryBasicWanderAI : MonoBehaviour
{
    enum AIStates
    {
        Idle,
        Wandering
    }

    [SerializeField]
    private NavMeshAgent agent = null;
    [SerializeField]
    private LayerMask floorMask = 0;

    private AIStates curState = AIStates.Idle;
    private float waitTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case AIStates.Idle:
                DoIdle();
                break;
            case AIStates.Wandering:
                DoWander();
                break;
            default:
                Debug.LogError("Should not be here, away with you! D:");
                break;
        }
    }

    private void DoIdle()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        agent.SetDestination(RandomNavSphere(transform.position, 10.0f, floorMask));
        curState = AIStates.Wandering;
    }

    private void DoWander()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathComplete)
            return;

        waitTimer = Random.Range(1.0f, 4.0f);
        curState = AIStates.Idle;
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }
}
