using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour {

    public GameObject target;
    public GameObject sphere;

    GameObject jitter;

    NavMeshAgent agent;
    Drive ds;
    Vector3 wanderTarget = Vector3.zero;

    float q = 0.0f;


    void Start() {

        agent = GetComponent<NavMeshAgent>();
        ds = target.GetComponent<Drive>();
        jitter = Instantiate(sphere);
    }

    void Seek(Vector3 location) {

        agent.SetDestination(location);
    }

    void Flee(Vector3 location) {

        Vector3 fleeVector = location - transform.position;
        agent.SetDestination(transform.position - fleeVector);
    }

    void Pursue() {

        Vector3 targetDir = target.transform.position - transform.position;
        float relativeHeading = Vector3.Angle(transform.forward, transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(transform.forward, transform.TransformVector(targetDir));


        if ((toTarget > 90.0f && relativeHeading < 20.0f) || ds.currentSpeed < 0.01f) {

            // Debug.Log("SEEKING");
            Seek(target.transform.position);
            return;
        }

        // Debug.Log("LOOKING AHEAD");
        float lookAhead = targetDir.magnitude / (agent.speed + ds.currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Evade() {

        Vector3 targetDir = target.transform.position - transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + ds.currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    void Wander() {

        float wanderRadius = 10.0f;
        float wanderDistance = 20.0f;
        float wanderJitter = 1.0f;

        wanderTarget += new Vector3(
            Random.Range(-1.0f, 1.0f) * wanderJitter,
            0.0f,
            Random.Range(-1.0f, 1.0f));
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0.0f, 0.0f, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        Debug.DrawLine(transform.position, targetWorld, Color.red);
        jitter.transform.position = targetWorld;
        Seek(targetWorld);
    }

    void Update() {

        // Seek(target.transform.position);
        // Flee(target.transform.position);
        // Pursue();
        // Evade();
        Wander();
    }

    void FixedUpdate() {
        // always draw a 5-unit colored line from the origin
        Color color = new Color(q, q, 1.0f);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 5, 0), color);
        q = q + 0.01f;

        if (q > 1.0f) {
            q = 0.0f;
        }
    }
}
