using UnityEngine;

namespace Services.Positions
{
    public interface IPositionsService
    {
        Vector3 UIToWorldPoint(Vector3 uiPosition);
        Vector3 ScreenToWorldPoint(Vector3 position, NaniCameraType cameraType = NaniCameraType.Main);
    }
}