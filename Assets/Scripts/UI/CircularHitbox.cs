using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CircularHitbox : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform, sp, eventCamera, out Vector2 local);

        var rt = (RectTransform)transform;
        float radius = Mathf.Min(rt.rect.width, rt.rect.height) * 0.5f;
        return local.magnitude <= radius;
    }
}