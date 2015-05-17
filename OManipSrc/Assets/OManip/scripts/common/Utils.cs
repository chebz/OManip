using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace cpGames
{
    public interface IRandomRoll
    {
        float Chance { get; }
    }

    public static class Utils
    {
        public static T AddChild<T>(GameObject parent, T prefab, Vector3 position, Quaternion rotation, Vector3 scale) where T : Component
        {
            T go = (T)GameObject.Instantiate(prefab);

            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;

                t.localPosition = position;
                t.localRotation = rotation;
                t.localScale = scale;
            }
            return go;
        }

        public static T AddChild<T>(GameObject parent, T prefab) where T : Component
        {
            return AddChild<T>(parent, prefab, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public static GameObject AddChild(GameObject parent, GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameObject go = (GameObject)GameObject.Instantiate(prefab);

            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.SetParent(parent.transform);

                t.localPosition = position;
                t.localRotation = rotation;
                t.localScale = scale;
            }
            return go;
        }

        public static GameObject AddChild(GameObject parent, GameObject prefab)
        {
            return AddChild(parent, prefab, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public static List<T> FindAllChildren<T>(Transform current) where T : Component
        {
            List<T> result = new List<T>();

            foreach (Transform child in current)
            {
                T component = child.GetComponent<T>();
                if (component != null)
                    result.Add(component);
            }

            return result;
        }

        public static Transform FindChildRecursively(Transform current, string name)
        {
            if (current.name == name)
                return current;

            for (int i = 0; i < current.childCount; ++i)
            {
                Transform found = FindChildRecursively(current.GetChild(i), name);

                if (found != null)
                    return found;
            }
            return null;
        }

        public static T FindChildRecursively<T>(Transform current, string name) where T : Component
        {
            if (current.name == name)
                return current.GetComponent<T>();

            for (int i = 0; i < current.childCount; ++i)
            {
                Transform found = FindChildRecursively(current.GetChild(i), name);

                if (found != null)
                    return found.GetComponent<T>();
            }
            return null;
        }

        public static T FindChildRecursively<T>(Transform current) where T : Component
        {
            T result = current.GetComponent<T>();

            if (result != null)
                return result;

            foreach (Transform child in current)
            {
                result = FindChildRecursively<T>(child);

                if (result != null)
                    return result;
            }
            return null;
        }

        public static List<T> FindAllChildrenRecursively<T>(Transform current) where T : Component
        {
            List<T> result = new List<T>();

            foreach (Transform child in current)
            {
                result.AddRange(FindAllChildrenRecursively<T>(child));
            }

            T component = current.GetComponent<T>();
            if (component != null)
                result.Add(component);

            return result;
        }

        public static List<T> FindAllChildrenRecursively<T>(Transform current, string name) where T : Component
        {
            List<T> result = new List<T>();

            foreach (Transform child in current)
            {
                result.AddRange(FindAllChildrenRecursively<T>(child, name));
            }

            if (current.name == name)
            {
                T component = current.GetComponent<T>();
                if (component != null)
                    result.Add(component);
            }

            return result;
        }

        public static void DeleteChildren(Transform transform)
        {
            List<GameObject> toDestroy = new List<GameObject>();
            foreach (Transform t in transform)
                toDestroy.Add(t.gameObject);
            toDestroy.ForEach(child => MonoBehaviour.Destroy(child));
        }

        public static T FindFirstParent<T>(Transform current) where T : Component
        {
            if (current == null)
                return null;

            T result = null;

            while (true)
            {
                result = current.GetComponent<T>();
                if (result != null)
                    return result;
                current = current.parent;
                if (current == null)
                    return null;
            }
        }

        public static T FindNextParent<T>(Transform current) where T : Component
        {
            if (current == null || current.parent == null)
                return null;

            current = current.parent;

            T result = null;

            while (true)
            {
                result = current.GetComponent<T>();
                if (result != null)
                    return result;
                current = current.parent;
                if (current == null)
                    return null;
            }
        }

        public static Bounds GetBoundsRecursively(Transform t)
        {
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            foreach (Transform child in t)
            {
                Bounds childBounds = GetBoundsRecursively(child);
                if (childBounds.extents.sqrMagnitude != 0)
                {
                    if (bounds.extents.sqrMagnitude == 0)
                        bounds = childBounds;
                    else
                        bounds.Encapsulate(childBounds);
                }
            }

            Renderer r = t.GetComponent<Renderer>();
            if (r != null)
            {
                if (bounds.extents.sqrMagnitude == 0)
                    bounds = r.bounds;
                else
                    bounds.Encapsulate(r.bounds);
            }

            return bounds;
        }

        public static bool Compare(int[] a, int[] b)
        {
            if (a == null || b == null)
                return a == b;

            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }

        public static bool Compare(float a, float b, float precision)
        {
            if (a == b)
                return true;
            if (Mathf.Abs(a - b) < precision)
                return true;
            return false;
        }

        public static bool Compare(Vector3 a, Vector3 b, float precision)
        {
            return Compare(a.x, b.x, precision) &&
                Compare(a.y, b.y, precision) &&
                Compare(a.z, b.z, precision);
        }

        public static bool Compare(Quaternion a, Quaternion b, float precision)
        {
            Quaternion rot = Quaternion.Inverse(a) * b;
            float angle;
            Vector3 axis;
            rot.ToAngleAxis(out angle, out axis);
            return angle < precision;
        }

        public static float CalcVerticalAngle(Vector2 start, Vector2 finish, float bulletVelocity)
        {
            float g = -Physics.gravity.y;
            float x = Mathf.Abs(finish.x - start.x);
            float y = finish.y - start.y;
            float v2 = bulletVelocity * bulletVelocity;
            float v4 = v2 * v2;
            float D = v4 - g * (g * x * x + 2 * y * v2);
            float sqrt = Mathf.Sqrt(D);
            float reqAngle1 = Mathf.Atan((v2 - sqrt) / (g * x)) * Mathf.Rad2Deg;
            return reqAngle1;
        }

        public static float ClampAngle(float angle)
        {
            angle = angle % 360;

            if (angle < -180)
                angle += 360;
            if (angle > 180)
                angle -= 360;

            return angle;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            return Mathf.Clamp(ClampAngle(angle), min, max);
        }

        public static bool IsOppositeSign(float a, float b)
        {
            if (a < 0 && b > 0)
                return true;
            if (a > 0 && b < 0)
                return true;
            return false;
        }

        public static Vector3 RelativePosition(Vector3 point, Transform t, Transform relativeTo)
        {
            Vector3 pos = t.TransformPoint(point);
            Vector3 relativePos = relativeTo.InverseTransformPoint(pos);
            return relativePos;
        }

        public static Vector2 ToVector2(this Vector3 input)
        {
            return new Vector2(input.x, input.y);
        }

        public static IRandomRoll PickRandom(List<IRandomRoll> collection)
        {
            float total = 0;
            foreach (var x in collection)
            {
                total += x.Chance;
            }

            float random = Random.Range(0, total);
            foreach (var x in collection)
            {
                random -= x.Chance;
                if (random <= 0)
                    return x;
            }
            return null;
        }

        public static List<B> CovariantCast<B, D>(List<D> derivedList) where D : B
        {
            List<B> baseList = new List<B>();
            derivedList.ForEach(x => baseList.Add(x));
            return baseList;
        }


#if UNITY_EDITOR
        public static bool IsObjectSelected(GameObject gob, int levels)
        {
            foreach (var selected in UnityEditor.Selection.gameObjects)
            {
                if (selected == gob)
                    return true;

                Transform current = gob.transform.parent;
                while (current != null && levels > 0)
                {
                    if (current.gameObject == selected)
                        return true;
                    current = current.parent;
                    levels--;
                }
            }
            return false;
        }

        public static bool AreSiblingsSelected(GameObject gob, int levels)
        {
            Transform parent = gob.transform.parent;

            while (parent != null && levels > 0)
            {
                foreach (Transform t in parent)
                {
                    foreach (var selected in UnityEditor.Selection.gameObjects)
                    {
                        if (t.gameObject == selected)
                            return true;
                    }
                }

                parent = parent.parent;
                levels--;
            }

            return false;
        }
#endif
    }
}