using ULB.RPG.FSM;

namespace ULB.RPG
{
    public class CharacterEnemy : CharacterBase
    {
        protected override CharacterStateMachine CreateMachine()
        {
            return new EnemyStateMachine(gameObject);
        }
    }
}