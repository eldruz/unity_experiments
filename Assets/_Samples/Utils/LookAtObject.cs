using UnityEngine;

public class LookAtObject : MonoBehaviour
{

    [SerializeField] private Transform toFollow;
    [SerializeField] private bool isSmooth;
    [SerializeField] private float smoothFactor = 1f;

    void Update()
    {
        if (!isSmooth)
        {
            transform.LookAt(toFollow);
        }
        else
        {
            Vector3 target = toFollow.position - transform.position;
            Quaternion rotateTo = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, Time.deltaTime * smoothFactor);
        }
    }
}
