using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceFrustrumDetection : MonoBehaviour
{
    public Camera cam;
    public ThiefStateManager thiefStateManager;
    public Collider thiefCollider;

    void Start()
    {
        if (thiefStateManager == null)
        {
            Debug.LogError("ThiefStateManager no asignado en PoliceFrustrumDetection.");
        }
    }

    void Update()
    {
        if (thiefStateManager == null) return;

        // Obtener los planos del frustum de la c�mara
        Plane[] cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(cam);
        

        // Comprobar si el ladr�n est� dentro del frustum de la c�mara
        if (GeometryUtility.TestPlanesAABB(cameraFrustrum, thiefCollider.bounds))
        {
            //El ladr�n s� est� en el FOV del policia
            switch (thiefStateManager.currentState)
            {
                case ThiefStateManager.ThiefState.Idle:
                    //No cambia nada

                    break;

                case ThiefStateManager.ThiefState.StealItem:
                    //Cambiar a modo Idle
                    thiefStateManager.currentState = ThiefStateManager.ThiefState.Idle;
                    break;

                case ThiefStateManager.ThiefState.EvadePolice:
                    //No pasa nada (Deberia seguir buscando la posici�n para esconderse) -> FindHidePosition()

                    break;

                case ThiefStateManager.ThiefState.Hide:
                    //Volver al modo EvadePolice (o volver a llamar a FindHidePosition())
                    thiefStateManager.currentState = ThiefStateManager.ThiefState.EvadePolice;
                    break;
            }
        }
        else
        {
            //El ladr�n no est� en el FOV del policia
            switch (thiefStateManager.currentState)
            {
                case ThiefStateManager.ThiefState.Idle:
                    //Cambiar a modo StealItem
                    thiefStateManager.currentState = ThiefStateManager.ThiefState.StealItem;
                    break;

                case ThiefStateManager.ThiefState.StealItem:
                    //No cambia nada

                    break;

                case ThiefStateManager.ThiefState.EvadePolice:
                    //No pasa nada (Deberia seguir buscando la posici�n para esconderse) -> FindHidePosition()

                    break;

                case ThiefStateManager.ThiefState.Hide:
                    //No pasa nada (Permanecer en el mismo sitio)

                    break;
            }

        }
    }
}
