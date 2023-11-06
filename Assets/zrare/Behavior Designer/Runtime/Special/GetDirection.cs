using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class GetDirection : Action
    {
        public SharedTransform _target;
        [RequiredField]
        public SharedInt dir;
        bool isEscape = false;
        public override TaskStatus OnUpdate()
        {
            dir.Value = isEscape ^ (_target.Value.position.x - transform.position.x) > 0 ? 1 : -1;
            return TaskStatus.Success;
        }
    }

}