using Infrastructure.Services;
namespace Background
{
    public interface IBackgroundAdjuster : IService
    {
        public float BackgroundsHeight { get; }
        public float VerticalOffset { get; }

        public float ResizeFactor { get; }
    }
}
