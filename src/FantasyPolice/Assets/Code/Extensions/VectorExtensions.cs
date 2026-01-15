using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, vector2.y, 0);
        }
        
        public static Vector3 ResetZ(this Vector3 v)
        {
            return SetZ(v, 0);
        }
        
        public static Vector3 SetX(this Vector3 v, float x)
        {
            var tmp = v;
            tmp.x = x;
            v = tmp;
            return v;
        }

        public static Vector3 SetY(this Vector3 v, float y)
        {
            var tmp = v;
            tmp.y = y;
            v = tmp;
            return v;
        }

        public static Vector3 SetZ(this Vector3 v, float z)
        {
            var tmp = v;
            tmp.z = z;
            v = tmp;
            return v;
        }
        
        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector2 SetX(this Vector2 v, float x)
        {
            var tmp = v;
            tmp.x = x;
            v = tmp;
            return v;
        }

        public static Vector2 SetY(this Vector2 v, float y)
        {
            var tmp = v;
            tmp.y = y;
            v = tmp;
            return v;
        }

        public static Vector3 AddX(this Vector3 v, float xDelta)
        {
            var tmp = v;
            tmp.x = tmp.x + xDelta;
            v = tmp;
            return v;
        }

        public static Vector3 AddY(this Vector3 v, float yDelta)
        {
            var tmp = v;
            tmp.y = tmp.y + yDelta;
            v = tmp;
            return v;
        }

        public static Vector2 AddX(this Vector2 v, float xDelta)
        {
            var tmp = v;
            tmp.x = tmp.x + xDelta;
            v = tmp;
            return v;
        }

        public static Vector2 AddY(this Vector2 v, float yDelta)
        {
            var tmp = v;
            tmp.y = tmp.y + yDelta;
            v = tmp;
            return v;
        }
    }
}