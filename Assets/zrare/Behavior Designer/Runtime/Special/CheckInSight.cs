using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class CheckInSight : Action
    {
        public SharedTransform _vec2;
        public SharedFloat sightDistance;

        public override TaskStatus OnUpdate()
        {   
            return sightDistance.Value > Vector2.Distance(new Vector2(_vec2.Value.position.x,_vec2.Value.position.y), transform.position) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

}
