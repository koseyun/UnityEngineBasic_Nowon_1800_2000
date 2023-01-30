using System;
using System.Collections;
using System.Collections.Generic;
using ULB.RPG.InputSystems;
using UnityEngine;

namespace ULB.RPG.FSM
{
    public class EnemyStateAttack : CharacterStateBase
    {
        private enum Step
        {
            Idle,
            Prepare,
            Action,
            Combo
        }
        private Step _step;

        public EnemyStateAttack(int id, GameObject owner, Func<bool> canExecute, List<KeyValuePair<Func<bool>, int>> transitions, bool hasExitTime) : base(id, owner, canExecute, transitions, hasExitTime)
        {
        }

        public override void Execute()
        {
            base.Execute();
            movement.mode = MovementBase.Mode.Manual;
            animator.SetBool("doAttack", true);
            _step = Step.Prepare;
        }

        public override void Stop()
        {
            base.Stop();
            animator.SetBool("doAttack", false);
        }

        public override int Update()
        {
            switch (_step)
            {
                case Step.Idle:
                    break;
                case Step.Prepare:
                    {
                        
                    }
                    break;
                case Step.Action:
                    {

                    }
                    break;
                case Step.Combo:
                    {
                        if (animator.isPreviousMachineFinished)
                        {
                            animator.SetBool("doCombo", false);
                            _step = Step.Prepare;
                        }
                    }
                    break;
                default:
                    break;
            }

            return id;
        }
    }
}