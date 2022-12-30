using Infrastructure.Services;
namespace Background
{
    public interface IScreenAdjustable : IService
    {
        public float BackgroundsHeight { get; }
        public float VerticalOffset { get; }
        
        public float ResizeFactor { get; }
    }
}
