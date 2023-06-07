using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : StaticInstance<CameraMovement>
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep = 1, minCamSize = 5, maxCamSize = 20, zoomDuration = 0.5f;

    private static Bounds MapBounds => PreyManager.Instance.SpawnArea;

    private Vector3 m_dragOrigin;

    void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            m_dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = m_dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    [ContextMenu(nameof(ZoomIn))]
    public void ZoomIn()
    {
        Zoom(cam.orthographicSize - zoomStep);
    }

    [ContextMenu(nameof(ZoomOut))]
    public void ZoomOut()
    {
        Zoom(cam.orthographicSize + zoomStep);
    }

    private void Zoom(float newSize)
    {
        cam.DOOrthoSize(Mathf.Clamp(newSize, minCamSize, maxCamSize), zoomDuration);
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        Vector3 min = MapBounds.min + new Vector3(camWidth, camHeight);
        Vector3 max = MapBounds.max - new Vector3(camWidth, camHeight);

        //mapBounds.SetMinMax(min, max);

        float newX = Mathf.Clamp(targetPosition.x, min.x, max.x);
        float newY = Mathf.Clamp(targetPosition.y, min.y, max.y);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
