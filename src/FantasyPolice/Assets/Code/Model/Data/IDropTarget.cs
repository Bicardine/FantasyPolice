using Services.Interactions;

namespace Model.Data
{
    public interface IDropTarget : IInteractor
    {
        bool CanAccept(IDrop drop);
        void Accept(IDrop drop);
    }
}
