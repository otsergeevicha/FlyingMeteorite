using Plugins.MonoCache;

namespace Infrastructure.Factory
{
    public class LeaderboardScreen : MonoCache
    {
        public void OnActive() =>
            gameObject.SetActive(true);

        public void InActive() =>
            gameObject.SetActive(false);
    }
}