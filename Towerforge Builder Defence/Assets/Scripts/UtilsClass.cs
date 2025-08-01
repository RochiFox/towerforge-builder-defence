using UnityEngine;

public static class UtilsClass
{
    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if (!mainCamera)
            mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDirection() => new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }
}
