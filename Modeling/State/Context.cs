using System;

namespace State
{
    public class Context
    {
        private IState _state;

        public Context(IState state)
        {
            State = state;
        }

        public IState State
        {
            get => _state;
            set
            {
                _state = value;
                Console.WriteLine($"State changed to: {_state.GetType().Name}");
            }
        }

        public void Request()
        {
            _state.Handle(this);
        }
    }
}
