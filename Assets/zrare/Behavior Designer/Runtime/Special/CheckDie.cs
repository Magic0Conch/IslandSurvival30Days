using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class CheckDie : Action
    {
        public int border_hp = 0;
        Animal animal;
        public override void OnAwake()
        {
            animal = GetComponent<Animal>();
        }
        public override TaskStatus OnUpdate()
        {
            
            return animal.nowHp <= border_hp ? TaskStatus.Success: TaskStatus.Failure;
        }
    }
}
