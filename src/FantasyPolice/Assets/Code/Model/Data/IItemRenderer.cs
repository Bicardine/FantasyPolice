using Cysharp.Threading.Tasks;

namespace Model.Data
{
    public interface IItemRenderer<TDataType>
    {
        void Render(TDataType data);
    }

    public interface IAsyncItemRenderer<TDataType>
    {
        UniTask RenderAsync(TDataType data);
    }
}