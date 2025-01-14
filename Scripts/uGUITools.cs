#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class uGUITools : MonoBehaviour
{
    [MenuItem("uGUI/Anchors to Corners %[")]
    static void AnchorsToCorners()
    {
        GameObject[] allSelectedObjects = Selection.gameObjects;
        foreach (GameObject rect in allSelectedObjects)
        {
            RectTransform t = rect.GetComponent<RectTransform>();
            RectTransform pt = rect.transform.parent.gameObject.GetComponent<RectTransform>();
            if (t == null || pt == null) return;
            Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                                                t.anchorMin.y + t.offsetMin.y / pt.rect.height);
            Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                                                t.anchorMax.y + t.offsetMax.y / pt.rect.height);
            t.anchorMin = newAnchorsMin;
            t.anchorMax = newAnchorsMax;
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }
    }
    [MenuItem("uGUI/Corners to Anchors %]")]
    static void CornersToAnchors()
    {
        GameObject[] allSelectedObjects = Selection.gameObjects;
        foreach (GameObject rect in allSelectedObjects)
        {
            RectTransform t = rect.GetComponent<RectTransform>();
            if (t == null) return;
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }
    }
}
#endif