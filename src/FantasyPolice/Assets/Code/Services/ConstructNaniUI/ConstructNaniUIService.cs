using Naninovel.UI;
using Zenject;

namespace Services.ConstructNaniUI
{
    /// <summary>
    /// Coz of the un-overrideable Nani factory injection is used.
    /// </summary>
    public class ConstructNaniUIService : IConstructNaniUIService
    {
        private readonly DiContainer _container;

        public ConstructNaniUIService(DiContainer container)
        {
            _container = container;
        }

        public void To(IManagedUI customUI)
        {
            _container.Inject(customUI);
        }
    }
}
