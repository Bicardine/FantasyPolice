namespace Model.Data
{
    public interface IChildClickable
    {
        void TryAcceptClick();
        bool CanAcceptClick();
    }
}