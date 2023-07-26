using Plugins.MonoCache;

namespace Infrastructure.Factory
{
    public class MenuScreen : MonoCache
    {
        public void Inject()
        {
            InActive();
        }

        public void OnActive() => 
            gameObject.SetActive(true);

        public void InActive() => 
            gameObject.SetActive(false);
    }
}