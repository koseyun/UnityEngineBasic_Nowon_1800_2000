using System;
using System.Collections.Generic;
using ULB.RPG.InputSysetems;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class PlayerStateMachine : CharacterStateMachine
    {
        public PlayerStateMachine(GameObject owner) : base(owner)
        {
        }

        public override void InitStates()
        {
            Rigidbody rb = owner.GetComponent<Rigidbody>();
            GroundDetector groundDetector = owner.GetComponent<GroundDetector>();

            IState move = new PlayerStateMove(id: (int)StateType.Move,
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

            IState jump = new PlayerStateJump(id: (int)StateType.Jump,
                                              owner: owner,
                                              canExecute: () => groundDetector.isDetected,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => rb.velocity.y <= 0 && groundDetector.isDetected,
                                                      (int)StateType.Land
                                                  ),
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => rb.velocity.y <= 0, //바꿀 조건
                                                      (int)StateType.Fall //바꿀 상태
                                                  ),
                                              },
                                              hasExitTime: false);
            states.Add(StateType.Jump, jump);

            IState fall = new PlayerStateFall(id: (int)StateType.Fall,
                                              owner: owner,
                                              canExecute: () => groundDetector.isDetected == false,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => groundDetector.isDetected,
                                                      (int)StateType.Land
                                                  ),
                                              },
                                              hasExitTime: false);
            states.Add(StateType.Fall, fall);

            IState land = new PlayerStateLand(id: (int)StateType.Land,
                                              owner: owner,
                                              canExecute: () => groundDetector.isDetected,
                                              transitions: new List<KeyValuePair<Func<bool>, int>>()
                                              {
                                                  new KeyValuePair<Func<bool>, int>
                                                  (
                                                      () => true,
                                                      (int)StateType.Fall
                                                  ),
                                              },
                                              hasExitTime: true);
            states.Add(StateType.Land, land);

            KeyInputHandler.instance.RegisterKeyPressAction(KeyCode.Space, () => ChangeState(StateType.Jump));
        }
    }
}
