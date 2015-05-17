using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace cpGames.Common
{
    [CustomEditor(typeof(TweenImageColor))]
    public class TweenImageAlphaEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!Application.isPlaying)
            {
                TweenImageColor tween = (TweenImageColor)target;
                Image image = tween.GetComponent<Image>();
                tween.colorFrom = image.color;
            }
        }
    }
}