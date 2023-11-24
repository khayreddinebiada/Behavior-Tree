using HCEngine.BT;
using UnityEngine;

public class Simple : MonoBehaviour
{
    private enum BehaviorType { Behavior1, Behavior2 }

    [SerializeField] private float _delay = 1;
    [SerializeField] private BehaviorType _behaviorType = BehaviorType.Behavior1;

    private BehaviorTree _behavior;

    private void Start()
    {
        switch (_behaviorType)
        {
            case BehaviorType.Behavior1:
                _behavior = Behavior1();
                break;
            case BehaviorType.Behavior2:
                _behavior = Behavior2();
                break;
        }


        /// To the tree graph please open the window on: Window/Behavior Tree.
        DisplayerTree.Display(_behavior);
    }

    private void Update()
    {
        if (_behavior.State == NodeState.Success)
            return;

        Debug.Log($"Last result: {_behavior.State}");
        _behavior.Tick();
        Debug.Log($"The new result is: {_behavior.State}");
    }

    private BehaviorTree Behavior1()
    {
        return BTreeBuilder.Create()
                        .Sequence()
                            .WaitTime(_delay)
                            .Action(DebugStart)
                            .Action(Action1)
                            .RepearUntilSuccess()
                                .Sequence()
                                    .Condition(AllowAction2)
                                    .Action(Action2)
                                .End()
                            .End()
                        .End()
                        .Build();
    }

    private BehaviorTree Behavior2()
    {
        return BTreeBuilder.Create()
                        .Sequence()
                            .WaitTime(_delay)
                            .Action(DebugStart)
                            .Repeat(10)
                                .Selector()
                                    .Sequence()
                                        .Inverter()
                                            .Condition(AllowSleep)
                                        .End()
                                        .Condition(AllowAction2)
                                        .Action(Action2)
                                    .End()
                                    .Sequence()
                                        .Action(Sleep)
                                    .End()
                                .End()
                            .End()
                        .Build();
    }

    private NodeState Action1()
    {
        Debug.Log("Action1");
        return NodeState.Success;
    }

    private NodeState DebugStart()
    {
        Debug.Log("Start");
        return NodeState.Success;
    }

    private bool AllowAction2()
    {
        Debug.Log("AllowAction2");
        return Random.Range(0, 10) == 0;
    }

    private NodeState Action2()
    {
        Debug.Log("Action2");
        return NodeState.Success;
    }

    private bool AllowSleep()
    {
        Debug.Log("AllowSleap");
        return Random.Range(0, 10) == 0;
    }

    private NodeState Sleep()
    {
        Debug.Log("Sleap");
        return NodeState.Success;
    }

    public void Enable()
    {
        _behavior.IsEnable = true;
    }

    public void Disable()
    {
        _behavior.IsEnable = false;
    }
}
