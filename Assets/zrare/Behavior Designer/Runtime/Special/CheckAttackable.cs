using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
    [TaskCategory("Special")]
    public class CheckAttackable : Action
    {
        [Tooltip("敌人一次攻击力，满血为100")]
        public float attackStrength = 15;
        public override void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("0");
            if(collision.transform.tag=="Player")
            {
                Debug.Log("1");
                BasicData.Instance.playerHp -= attackStrength;
                GetComponent<Collider2D>().isTrigger = true;
                GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }

        public override TaskStatus OnUpdate()
        {
            Debug.Log("up");
            return TaskStatus.Success;
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("2");

            if (other.transform.tag == "Player")
            {
                Debug.Log("3");

                //GetComponent<Collider2D>().isTrigger = false;
                //GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }

        public override void OnReset()
        {
            Debug.Log("reset");

            //GetComponent<Collider2D>().isTrigger = false;
            //GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

}
