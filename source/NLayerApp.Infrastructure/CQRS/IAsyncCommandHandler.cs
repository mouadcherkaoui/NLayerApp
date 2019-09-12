using System.Threading.Tasks;

namespace NLayerApp.Infrastructure.CQRS
{
    public interface IAsyncCommandHandler<TResult>
    {
        Task<TResult> ExecuteAsync();
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