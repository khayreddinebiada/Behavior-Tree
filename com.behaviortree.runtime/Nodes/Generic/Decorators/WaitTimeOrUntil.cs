using HCEngine.BT;
using System;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace HCEngine.BT.Generic
{
    public class WaitTimeOrUntil : Decorator
    {
        private NodeState RequiredState { get; set; }

        private int _version = 0;
        public bool IsTimeScaled { get; set; }
        public float TimeRequired { get; set; }
        protected float StartedTime { get; private set; }

        public float DeltaTime
            => CurrentTime() - StartedTime;

        public WaitTimeOrUntil(NodeState requiredState, float timeRequired, bool isTimeScaled = true)
            : base()
        {
            RequiredState = requiredState;
            TimeRequired = timeRequired;
            IsTimeScaled = isTimeScaled;
        }

        public override void OnStarted()
        {
            _version = Version;
            StartedTime = CurrentTime();
        }

        protected override NodeState Evaluate()
        {
            if (_version != Version)
                return NodeState.Abort;

            if (Child.IsNull())
                throw new NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            NodeState newState = Child.Tick();
            if (newState == RequiredState)
                return newState;

            if (CurrentTime() < TimeRequired + StartedTime)
                return NodeState.Running;

            return NodeState.Success;
        }


        private float CurrentTime()
        {
            return (IsTimeScaled) ? Time.time : Time.unscaledTime;
        }
    }
}