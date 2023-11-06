using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
    [TaskCategory("Special")]
    public class DistanceTran : Action
    {
        [Tooltip("The first Vector2")]
        public SharedTransform _trans;
        [Tooltip("The second Transform")]
        public SharedVector2 _vec2;
        [Tooltip("The distance")]
        [RequiredField]
        public SharedFloat storeResult;
        public override TaskStatus OnUpdate()
        {
            storeResult.Value = Vector2.Distance(_trans.Value.position, _vec2.Value);
            return TaskStatus.Success;
        }
    }

}
