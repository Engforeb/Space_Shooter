using Infrastructure.Services;
namespace Background.Infrastructure.States
{
    public interface IScreenAdjustable : IService
    {
        public float BackgroundsHeight { get; }
        public float VerticalOffset { get; }
        
        public float ResizeFactor { get; }
    }
}
