using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace HCEngine.BT.Generic
{
    public class SelectorRandom : Composite
    {
        private int _version = 0;
        private int _currentIndex = 0;
        private List<int> indexes = new List<int>();

        public override void OnStarted()
        {
            indexes = Enumerable.Range(0, CountChildren).ToList();
            _currentIndex = NextIndex();
            _version = Version;
        }

        protected override NodeState Evaluate()
        {
            while (0 <= indexes.Count)
            {
                if (_version != Version)
                    return NodeState.Abort;

                INode current = this[_currentIndex];

                if (current.IsNull())
                    throw new NullReferenceException(nameof(current));
                Contract.EndContractBlock();


                NodeState newState = current.Tick();
                if (newState != NodeState.Failure)
                    return newState;

                _currentIndex = NextIndex();
            }

            return NodeState.Failure;
        }

        private int NextIndex()
        {
            int randomIndex = UnityEngine.Random.Range(0, indexes.Count);
            indexes.RemoveAt(randomIndex);
            return indexes[randomIndex];
        }
    }
}