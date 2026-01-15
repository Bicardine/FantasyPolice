using Services.Positions;
using UnityEngine;

namespace Model.Data
{
    public interface ICamera : IHoldCamera
    {
        new NaniCameraType CameraType => NaniCameraType.Main;
        NaniCameraType IHoldCamera.CameraType => CameraType;
    }

    public interface IUICamera : IHoldCamera
    {
        new NaniCameraType CameraType => NaniCameraType.UI;
        NaniCameraType IHoldCamera.CameraType => CameraType;
    }

    public interface IHoldCamera
    {
        public Camera Value { get; }
        NaniCameraType CameraType { get; }
    }
}