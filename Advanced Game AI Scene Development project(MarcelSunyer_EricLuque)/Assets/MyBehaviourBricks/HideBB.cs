using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("MyActions/Hide")]
[Help("Get the Vector3 for hiding.")]
public class HideBB : BasePrimitiveAction
{
    [InParam("game object")]
    public GameObject targetGameobject;

    [OutParam("hide")]
    public Vector3 hide;

    public override TaskStatus OnUpdate()
    {
        Moves moves = targetGameobject.GetComponent<Moves>();
        if (moves != null)
        {
            moves.Hide();
            hide = moves.hideValue;
            return TaskStatus.COMPLETED;
        }
        else
        {
            Debug.LogError("Moves component not found on targetGameobject.");
            return TaskStatus.FAILED;
        }
    }
}
