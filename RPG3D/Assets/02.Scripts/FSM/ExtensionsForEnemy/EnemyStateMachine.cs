using System;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class EnemyStateMachine : CharacterStateMachine
    {
        public EnemyStateMachine(GameObject owner) : base(owner)
        {
        }

        public override void InitStates()
        {
            Rigidbody rb = owner.GetComponent<Rigidbody>();
            GroundDetector groundDetector = owner.GetComponent<GroundDetector>();

            IState move=new EnemyStateMove(id: (int)StateType.Move,
                                           owner: owner,
                                           canExecute: () => true,
                                           transitions: new List<KeyValuePair<Func<bool>, int>>()
                                           {
                                               new KeyValuePair<Func<bool>, int>
                                               (
                                                   () => false,
                                                   (int)default(StateType)
                                               ),
                                           },
                                           hasExitTime: false);
            states.Add(StateType.Move, move);


        }
    }
}