using UnityEngine;

namespace HCEngine.BT.Generic
{
    public class WaitTime : Node
    {
        public bool IsTimeScaled { get; set; }
        public float TimeRequired { get; set; }
        public float DeltaTime => CurrentTime() - StartedTime;

        protected float StartedTime { get; private set; }

        public WaitTime(float timeRequired, bool isTimeScaled = true)
            : base()
        {
            TimeRequired = timeRequired;
            IsTimeScaled = isTimeScaled;
        }

        public override void OnStarted()
        {
            StartedTime = CurrentTime();
        }

        protected override NodeState Evaluate()
        {
            return (TimeRequired + StartedTime <= CurrentTime()) ? NodeState.Success : NodeState.Running;
        }

        protected float CurrentTime()
        {
            return (IsTimeScaled) ? Time.time : Time.unscaledTime;
        }
    }
}