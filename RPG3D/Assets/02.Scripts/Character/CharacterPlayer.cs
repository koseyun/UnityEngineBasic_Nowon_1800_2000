using ULB.RPG.FSM;

namespace ULB.RPG.FSM
{
    public class CharacterPlayer : CharacterBase
    {
        protected override CharacterStateMachine CreateMachine()
        {
            return new PlayerStateMachine(gameObject);
        }
    }
}
