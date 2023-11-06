using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class Movetoward: Action
    {
        
        public SharedFloat speed;
        //public bool isEscape = false;
        public SharedInt direction;
        public bool isFlip = false;

        public override TaskStatus OnUpdate()
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = isFlip ^ direction.Value < 0;
            transform.Translate(Vector2.right * direction.Value * speed.Value * Time.fixedDeltaTime);
            return TaskStatus.Success;
        }

    }

}