
namespace Command
{
    public interface ICommand
    {
        void Execute();
    }

    public class ConcreteCommand : ICommand
    {
        private Receiver _receiver;

        public ConcreteCommand(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute()
        {
            _receiver.Action();
        }
    }
}
