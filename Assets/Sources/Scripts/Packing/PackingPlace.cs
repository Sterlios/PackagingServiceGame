using UnityEngine;

[System.Serializable]
class PackingPlace
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Quaternion _rotation;

    public void SetPositionAndRotation(Transform placeTransform, Transform targetTransform)
    {
        Vector3 position = GetWorldPosition(placeTransform);
        Quaternion rotation = GetWorldRotation(placeTransform);

        targetTransform.SetPositionAndRotation(position, rotation);
    }

    private Vector3 GetWorldPosition(Transform currentTransform)
    {
        Vector3 position = _position;

        for (Transform transform = currentTransform; transform != null; transform = transform.parent)
            if (transform.position != Vector3.zero)
                position = transform.position + transform.rotation * _position;

        return position;
    }

    private Quaternion GetWorldRotation(Transform currentTransform)
    {
        return Quaternion.Euler(_rotation.eulerAngles + currentTransform.rotation.eulerAngles);
    }
}
