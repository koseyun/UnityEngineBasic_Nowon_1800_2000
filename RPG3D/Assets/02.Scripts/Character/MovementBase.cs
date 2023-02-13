using UnityEngine;

namespace ULB.RPG
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public abstract class MovementBase : MonoBehaviour
    {
        public enum Mode
        {
            None,
            Manual,
            RootMotion
        }
        public Mode mode
        {
            get
            {
                return _mode;
            }
            set
            {
                if (_mode == value)
                    return;
    
                inertia = rb.velocity;
                _mode = value;
            }
        }
        private Mode _mode;
        protected Vector3 inertia;
        private float _drag;
        [SerializeField] private Animator _animator;

        public abstract float v { get; }
        public abstract float h { get; }
        public abstract float gain { get; }
        [SerializeField] protected Rigidbody rb;

        public virtual void SetMove(float v, float h, float gain) { }

        private void Awake()
        { 
            _drag = rb.drag;
            _mode = Mode.RootMotion;
        }
    
        private void Update()
        {
            if (_mode == Mode.RootMotion)
            {
                _animator.SetFloat("h", h * gain);
                _animator.SetFloat("v", v * gain);
            }        
        }
    
        private void FixedUpdate()
        {
            if (_mode == Mode.Manual)
            {
                rb.position += inertia * Time.fixedDeltaTime;
                inertia = Vector3.Lerp(inertia, Vector3.zero, _drag * Time.fixedDeltaTime);
            }
        }
    }
}
