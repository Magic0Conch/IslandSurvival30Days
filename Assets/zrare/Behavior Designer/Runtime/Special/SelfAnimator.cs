﻿using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic
{
    [TaskCategory("Special")]
    public class SelfAnimatior : Action
    {
        [Tooltip("The name of the state")]
        public SharedString stateName;
        [Tooltip("The layer where the state is")]
        public int layer = -1;
        [Tooltip("The normalized time at which the state will play")]
        public float normalizedTime = float.NegativeInfinity;

        private Animator animator;
        private GameObject prevGameObject;
        AnimatorStateInfo animatorInfo;
        public override void OnStart()
        {
                animator = GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }
            animator.Play(stateName.Value, layer, normalizedTime);
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorInfo.normalizedTime < 1.0f)
            {
                return TaskStatus.Running;
            }
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            stateName = "";
            layer = -1;
            normalizedTime = float.NegativeInfinity;
        }
    }
}
