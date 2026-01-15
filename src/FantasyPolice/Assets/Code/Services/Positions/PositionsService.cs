using System;
using Model.Data;
using UnityEngine;

namespace Services.Positions
{
    public class PositionsService : IPositionsService
    {
        private readonly ICamera _camera;
        private readonly IUICamera _uiCamera;

        public PositionsService(ICamera camera, IUICamera uiCamera)
        {
            _camera = camera;
            _uiCamera = uiCamera;
        }

        public Vector3 UIToWorldPoint(Vector3 uiPosition) =>
            _camera.Value.ScreenToWorldPoint(_uiCamera.Value.WorldToScreenPoint(uiPosition));

        public Vector3 ScreenToWorldPoint(Vector3 position, NaniCameraType cameraType = NaniCameraType.Main)
        {
            var camera = CameraByType(cameraType);
            return camera.Value.ScreenToWorldPoint(position);
        }

        private IHoldCamera CameraByType(NaniCameraType cameraType)
        {
            return cameraType switch
            {
                NaniCameraType.Main => _camera,
                NaniCameraType.UI => _uiCamera,
                _ => throw new ArgumentOutOfRangeException($"Unknown CameraType: {cameraType}.")
            };
        }
    }

    public enum NaniCameraType
    {
        Unknown,
        Main,
        UI
    }
}
