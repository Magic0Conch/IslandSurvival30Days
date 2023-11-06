#if !UNITY_5_0

using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
    public class IsServer : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Failure;
        }
    }
}
#endif