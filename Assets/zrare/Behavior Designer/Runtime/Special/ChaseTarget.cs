using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class ChaseTarget: Action
    {
        public SharedTransform _target;
        public SharedFloat speed;
        public bool isEscape = false;
        public bool isFlip = false;
        public bool isParent = false;
        int dir;

        public override TaskStatus OnUpdate()
        {
            dir = isEscape^(_target.Value.position.x - transform.position.x ) > 0 ? 1 : -1;
            gameObject.GetComponent<SpriteRenderer>().flipX = isFlip^ dir < 0;
            if(!isParent)
                transform.Translate(Vector2.right * dir * speed.Value * Time.deltaTime);
            else
                transform.parent.transform.Translate(Vector2.right * dir * speed.Value * Time.deltaTime);

            return TaskStatus.Success;
        }
    }

}

//ChaseTarget