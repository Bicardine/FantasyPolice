using Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiersBased
{
    public class ModifierView : MonoBehaviour, IItemRenderer<Sprite>
    {
        [SerializeField] private Image _image;
        public void Render(Sprite data)
        {
            _image.sprite = data;
        }
    }
}