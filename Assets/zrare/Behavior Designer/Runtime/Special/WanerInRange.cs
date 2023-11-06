using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class WanerInRange : Action
    {
        public SharedFloat speed;
        int dir;
        public float randomInterval = 2f;
        public bool isFilp = false;
        public float range = 1.6f;
        float oriX;
        public override void OnAwake()
        {
            StartCoroutine(randomDir());
            oriX = transform.position.x;
        }

        IEnumerator randomDir()
        {
            WaitForSeconds wfs = new WaitForSeconds(randomInterval);
            while (true)
            {
                dir = Random.value > 0.5 ? 1 : -1;
                yield return wfs;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (transform.position.x >= oriX + range) dir = -1;
            else if (transform.position.x <= oriX - range) dir = 1;
            gameObject.GetComponent<SpriteRenderer>().flipX = isFilp ^ dir < 0;
            transform.Translate(Vector2.right * dir * speed.Value * Time.deltaTime);
            return TaskStatus.Success;
        }
    }

}
//WanerInRange