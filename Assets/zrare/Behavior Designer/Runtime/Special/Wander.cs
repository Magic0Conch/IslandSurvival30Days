using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class Wander : Action
    {
        public SharedFloat speed;
        int dir;
        public float randomInterval = 2f;
        public bool isFilp = false;
        public bool isParent = false;
        public override void OnAwake()
        {
            StartCoroutine(randomDir());
        }
        
        IEnumerator randomDir()
        {
            WaitForSeconds wfs = new WaitForSeconds(randomInterval);
            while(true)
            {
                dir = Random.value > 0.5 ? 1 : -1;
                yield return wfs;
            }
        }

        public override TaskStatus OnUpdate()
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = isFilp^dir < 0;
            if(!isParent)
                transform.Translate(Vector2.right * dir * speed.Value * Time.deltaTime);
            else
                transform.parent.transform.Translate(Vector2.right * dir * speed.Value * Time.deltaTime);
            return TaskStatus.Success;
        }
    }

}

//ChaseTarget