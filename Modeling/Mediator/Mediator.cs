
namespace Mediator
{
    public interface IMediator
    {
        void Send(string message, Colleague colleague);
    }

    public class ConcreteMediator : IMediator
    {
        private ConcreteColleague1 _colleague1;
        private ConcreteColleague2 _colleague2;

        public ConcreteColleague1 Colleague1
        {
            set { _colleague1 = value; }
        }

        public ConcreteColleague2 Colleague2
        {
            set { _colleague2 = value; }
        }

        public void Send(string message, Colleague colleague)
        {
            if (colleague == _colleague1)
            {
                _colleague2.Receive(message);
            }
            else if (colleague == _colleague2)
            {
                _colleague1.Receive(message);
            }
        }
    }
}
