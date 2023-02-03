using UnityEngine;

[System.Serializable]
class PackingPlace
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Quaternion _rotation;

    public Vector3 GetWorldPosition(Transform currentTransform)
    {
        Vector3 position = _position;

        for (Transform transform = currentTransform; transform != null; transform = transform.parent)
            if (transform.position != Vector3.zero)
                position = transform.position + transform.rotation * _position;

        return position;
    }

    public Quaternion GetWorldRotation(Transform currentTransform)
    {
        return Quaternion.Euler(_rotation.eulerAngles + currentTransform.rotation.eulerAngles);
    }
}
