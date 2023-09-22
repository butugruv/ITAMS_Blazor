namespace BlazorApp.Services
{
    public class BannerService
    {
        public bool ShowBanner { get; private set; } = true;
        public event Action OnChange;

        public void HideBanner()
        {
            ShowBanner = false;
            NotifyStateChanged();
        }

        public void ShowBannerAgain()
        {
            ShowBanner = true;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
