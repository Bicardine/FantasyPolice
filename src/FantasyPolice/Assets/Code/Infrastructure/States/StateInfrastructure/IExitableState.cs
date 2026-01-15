using RSG;

namespace Infrastructure.States.StateInfrastructure
{
    public interface IExitableState
    {
        IPromise BeginExit();
        void EndExit();
    }
}