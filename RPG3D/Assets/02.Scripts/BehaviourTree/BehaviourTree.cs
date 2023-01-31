namespace ULB.RPG.AISystems
{
    public abstract class BehaviourTree
    {
        private Root _root;

        public void Tick()
        {
            _root.Invoke();
        }
    }
}