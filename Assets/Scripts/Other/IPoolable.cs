
public interface IPoolable
{
    event System.Action<IPoolable> OnPolableDestroy;
    void Reset();
}
