using UnityEngine;

[System.Serializable]
class PackingPlace
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Quaternion _rotation;

    public Vector3 GetWorldPosition(Transform transform)
    {
        Vector3 position = _position;

        for (Transform t = transform; t != null; t = t.parent)
            if (t.position != Vector3.zero)
                position = t.position + t.rotation * _position;

        return position;
    }

    public Quaternion GetWorldRotation(Transform transform)
    {
        return Quaternion.Euler(_rotation.eulerAngles + transform.rotation.eulerAngles);
    }
}
