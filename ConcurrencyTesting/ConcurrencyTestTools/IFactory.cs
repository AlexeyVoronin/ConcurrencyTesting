namespace Asteros.Abc.Common.Factories
{
    public interface IFactory<in TKey, out TValue>
    {
        TValue Create(TKey key);
    }
}
