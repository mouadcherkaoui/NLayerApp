using System.Threading.Tasks;

namespace NLayerdApp.Infrastructure.CQRS
{
    public interface IAsyncCommand<out TResult>
    {
        TResult ExecuteAsync();
    }
    public interface IAsyncCommand<T, out TResult>
    {
        TResult ExecuteAsync(T parameter);
    }
    public interface IAsyncCommand<T1, T2, out TResult>
    {
        TResult ExecuteAsync(T1 parameter1, T2 parameter2);
    }
    public interface IAsyncCommand<T1, T2, T3, out TResult>
    {
        TResult ExecuteAsync(T1 parameter1, T2 parameter2, T3 parameter3);
    }
}