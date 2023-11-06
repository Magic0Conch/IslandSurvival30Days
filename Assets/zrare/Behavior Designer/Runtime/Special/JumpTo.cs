using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
    [TaskCategory("Special")]
    public class JumpTo : Action
    {
        Rigidbody2D _rigid;
        public SharedTransform _target;
        public SharedVector2 velocity;

        public override void OnAwake()
        {
            if (_rigid == null)
                _rigid = GetComponent<Rigidbody2D>();
        }

        public override void OnStart()
        {
            int dir = _target.Value.position.x - transform.position.x > 0 ? 1 : -1;
            _rigid.velocity = new Vector2(velocity.Value.x * dir, velocity.Value.y);
        }
        
    }

}
