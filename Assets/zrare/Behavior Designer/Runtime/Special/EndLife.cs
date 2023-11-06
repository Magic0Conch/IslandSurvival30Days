using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Special")]
    public class EndLife : Action
    {
        public GameObject[] dropItem;
        public bool isParent = false;
        
        public override void OnStart()
        {
            int len = dropItem.Length;
            for(int i = 0;i<len;i++)
                GameObject.Instantiate(dropItem[i],transform.position,Quaternion.Euler(Vector3.zero));
            if (!isParent)
                GameObject.Destroy(gameObject);
            else
                GameObject.Destroy(transform.parent.gameObject);
        }
    }
}
