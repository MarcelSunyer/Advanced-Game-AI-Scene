using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Cop Near?")]
[Help("Checks whether Cop is near the Treasure.")]
public class IsCopNear : ConditionBase
{
    public string copTag = "Cop";
    public string treasureTag = "Treasure";

    private GameObject cop;
    private GameObject treasure;

    public override bool Check()
    {
        if (cop == null || treasure == null)
        {
            // Intenta encontrar los objetos si no están asignados
            FindObjectsWithTag();
        }

        if (cop != null && treasure != null)
        {
            return Vector3.Distance(cop.transform.position, treasure.transform.position) < 10f;
        }
        else
        {
            Debug.LogError("Cop or Treasure not found or assigned in the Inspector.");
            return false;
        }
    }

    private void FindObjectsWithTag()
    {
        cop = GameObject.FindGameObjectWithTag(copTag);
        treasure = GameObject.FindGameObjectWithTag(treasureTag);
    }
}
