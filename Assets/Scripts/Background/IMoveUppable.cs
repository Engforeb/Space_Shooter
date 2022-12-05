namespace Background
{
    public interface IMoveUppable
    {
        public void Init(float backgroundHeight, float offset);
        public void Move();
        public void MoveUp();
    }
}
