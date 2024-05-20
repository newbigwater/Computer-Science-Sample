
namespace AbstractFactory
{
    public interface IAbstractFactory
    {
        IAbstractProduct CreateProduct1();
        IAbstractProduct CreateProduct2();
    }

    public interface IAbstractProduct
    {
        void insertData(string data);
        void updateData(string data);
        void deleteData(string data);
    }
}
