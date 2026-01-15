using Model.Data;

namespace Nani.Cameras
{
    public class Camera : ICamera
    {
        public UnityEngine.Camera Value { get; }

        public Camera(UnityEngine.Camera camera)
        {
            Value = camera;
        }
    }

    public class UICamera : IUICamera
    {
        public UnityEngine.Camera Value { get; }

        public UICamera(UnityEngine.Camera camera)
        {
            Value = camera;
        }
    }
}
