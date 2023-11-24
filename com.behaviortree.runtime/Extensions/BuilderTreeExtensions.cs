using HCEngine.BT.Generic;
using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT
{
    public static class BuilderTreeExtensions
    {
        public static BehaviorTree Build(this BTreeBuilder builder)
        {
            return new BehaviorTree(builder.Root);
        }

        public static BTreeBuilder Join(this BTreeBuilder builder, BTreeBuilder otherBuilder)
        {
            builder.Bottom.Attach(otherBuilder.Root);
            return builder;
        }

        public static BTreeBuilder Join(this BTreeBuilder builder, INode node)
        {
            builder.Bottom.Attach(node);
            return builder;
        }

        public static BTreeBuilder End(this BTreeBuilder builder)
        {
            builder.Bottom = builder.Bottom.Parent;
            return builder;
        }

        public static BTreeBuilder AttachDecorator(this BTreeBuilder builder, IDecorator decorator, string name = null)
        {
            if (decorator == null)
                throw new ArgumentNullException($"Object name {nameof(decorator)}");
            Contract.EndContractBlock();

            decorator.Name = name;

            if (builder.Root == null)
                builder.Bottom = builder.Root = decorator;
            else
            {
                builder.Bottom.Attach(decorator);
                builder.Bottom = decorator;
            }

            return builder;
        }

        public static BTreeBuilder AttachNode(this BTreeBuilder builder, INode node, string name = null)
        {
            if (node == null)
                throw new ArgumentNullException($"Object name {nameof(node)}");
            Contract.EndContractBlock();

            node.Name = (name == null) ? "Node" : name;
            if (builder.Root == null)
            {
                Contract.Requires(node is IDecorator,
                    "You can't use non IDecorator type as a root!...");

                builder.Bottom = builder.Root = (IDecorator)node;
            }
            else
            {
                builder.Bottom.Attach(node);
            }

            return builder;
        }

        public static BTreeBuilder Sequence(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new Sequence(), "Sequence");
        }

        public static BTreeBuilder Selector(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new Selector(), "Selector");
        }

        public static BTreeBuilder Parallel(this BTreeBuilder builder, Policy successPolicy, Policy failurePolicy = Policy.Non)
        {
            return AttachDecorator(builder, new Parallel(successPolicy, failurePolicy), "Parallel");
        }

        public static BTreeBuilder Validator(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new Validator(), "Validator");
        }

        public static BTreeBuilder RepeatUntilAll(this BTreeBuilder builder, NodeState requiredState)
        {
            return AttachDecorator(builder, new RepeatUntilAll(requiredState), "RepearUntilSuccess");
        }

        public static BTreeBuilder Loop(this BTreeBuilder builder, NodeState requiredState, int totalCount)
        {
            return AttachDecorator(builder, new Loop(requiredState, totalCount), "Loop");
        }

        public static BTreeBuilder Loop(this BTreeBuilder builder,int totalCount)
        {
            return AttachDecorator(builder, new Loop(totalCount), "Loop");
        }

        public static BTreeBuilder Foreach(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new Foreach(), "Foreach");
        }

        public static BTreeBuilder Inverter(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new Inverter(), "Inverter");
        }

        public static BTreeBuilder RepearUntil(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new RepearUntilSuccess(), "RepearUntilSuccess");
        }
        
        public static BTreeBuilder RepeatForever(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new RepeatForever(), "RepeatForever");
        }

        public static BTreeBuilder RepeatUntilFailure(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new RepeatUntilFailure(), "RepeatUntilFailure");
        }

        public static BTreeBuilder RepearUntilSuccess(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new RepearUntilSuccess(), "RepearUntilSuccess");
        }

        public static BTreeBuilder Repeat(this BTreeBuilder builder, int limit)
        {
            return AttachDecorator(builder, new Repeat(limit), "Repeat");
        }

        public static BTreeBuilder ReturnFailure(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new ReturnFailure(), "ReturnFailure");
        }

        public static BTreeBuilder ReturnSuccess(this BTreeBuilder builder)
        {
            return AttachDecorator(builder, new ReturnSuccess(), "ReturnSuccess");
        }

        public static BTreeBuilder WaitTimeOrUntil(this BTreeBuilder builder, NodeState requiredState, float time, bool isTimeScaled = true)
        {
            return AttachDecorator(builder, new WaitTimeOrUntil(requiredState, time, isTimeScaled), "WaitTimeOrUntil");
        }

        public static BTreeBuilder Condition(this BTreeBuilder builder, Func<bool> condition, string name = null)
        {
            return AttachNode(builder, new Condition(condition), name);
        }

        public static BTreeBuilder Action(this BTreeBuilder builder, Func<NodeState> condition, string name = null)
        {
            return AttachNode(builder, new Generic.Action(condition), name);
        }

        public static BTreeBuilder WaitTime(this BTreeBuilder builder, float time, bool isTimeScaled = true)
        {
            return AttachNode(builder, new WaitTime(time, isTimeScaled), "WaitTime");
        }
    }
}