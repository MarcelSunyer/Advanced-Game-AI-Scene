 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefStateManager : MonoBehaviour
{
    public enum ThiefState
    {
        Idle,           //Movimiento randomspeed
        StealItem,      //Moverse hacia el item para robarlo
        EvadePolice,    //Buscar un punto válido donde esconderse
        Hide            //Ir el punto válido para esconderse y quedarse allí
    }

    public ThiefState currentState;
    public NavMeshAgent agent;
    public Transform itemPosition;
    public float hideSearchRadius = 10f;
    public float stealDistance = 1f;
    public Collider camaraCollider;

    public GameObject destruir;
    public bool hasStolenItem = false;
    public GameObject policeTarget;
    public Camera police_camera;
    public Collider floor;
    GameObject[] hidingSpots;
    private Transform hidePosition;
    private GameObject hidePositionGO;

   

    bool cameraLooking;
    float dotProductResult;

    void Start()
    {
        currentState = ThiefState.Idle;
        agent = GetComponent<NavMeshAgent>();
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
        hidePositionGO = new GameObject("HidePosition");
        GoToRandomPosition();
    }

    void Update()
    {
        switch (currentState)
        {
            case ThiefState.Idle:
                //Movimiento random
                //if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GoToRandomPosition();
                break;

            case ThiefState.StealItem:
                //Moverse hacia el item para robarlo

                if (hasStolenItem)
                {
                    currentState = ThiefState.EvadePolice;
                    
                }
                else
                {
                    agent.SetDestination(itemPosition.position);
                    
                    if (Vector3.Distance(transform.position, itemPosition.position) < 1f)
                    {
                        Debug.Log("Item Stolen!");
                        Destroy(destruir);
                        hasStolenItem = true;
                        currentState = ThiefState.EvadePolice;
                    }
                }

                break;

            case ThiefState.EvadePolice:
                //Buscar un punto válido donde esconderse

                Debug.Log("ThiefState.EvadePolice");

                if (hidePosition == null || agent.remainingDistance < 0.5f)
                {
                    hidePosition = FindHidePosition();
                    if (hidePosition != null)
                    {
                        agent.SetDestination(hidePosition.position);
                        currentState = ThiefState.Hide;
                    }
                    else
                    {
                        Debug.LogError("No se pudo encontrar una posición para esconderse");
                    }
                }

                break;

            case ThiefState.Hide:
                //Ir el punto válido para esconderse y quedarse allí

                Debug.Log("ThiefState.Hide");

                break;
        }

    }

    public Transform FindHidePosition()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = hidingSpots[0];
        if (policeTarget != null)
        {
            if (hidingSpots != null && hidingSpots.Length > 0)
            {
                for (int i = 0; i < hidingSpots.Length - 15; i++)
                {
                    Vector3 hideDir = hidingSpots[i].transform.position - policeTarget.transform.position;
                    Vector3 hidePos = hidingSpots[i].transform.position + hideDir.normalized * 100;

                    if (Vector3.Distance(policeTarget.transform.position, hidePos) < dist)
                    {
                        chosenSpot = hidePos;
                        chosenDir = hideDir;
                        chosenGO = hidingSpots[i];
                        dist = Vector3.Distance(this.transform.position, hidePos);
                    }
                }
            }

        }
        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 250.0f;
        backRay.direction.Normalize();
        hideCol.Raycast(backRay, out info, distance);

        hidePositionGO.transform.position = info.point + chosenDir.normalized;
        return hidePositionGO.transform;
        
    }

    void GoToRandomPosition()
    {
        float randomX = Random.Range(-hideSearchRadius, hideSearchRadius);
        float randomZ = Random.Range(-hideSearchRadius, hideSearchRadius);
        Vector3 randomPosition = new Vector3(randomX, 0, randomZ);

        agent.SetDestination(randomPosition);
    }
  
}
