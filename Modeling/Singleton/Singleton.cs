
namespace Singleton
{
    public class Singleton
    {
        private static Singleton _instance = null;

        Singleton() { }

        public static Singleton GetInstance()
        {
            if (null == _instance) _instance = new Singleton();

            return _instance;
        }
    }
}
