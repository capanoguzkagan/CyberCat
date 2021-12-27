using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm : MonoBehaviour
{
    public Vector3 rightDistance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameManager.Instance.RightArmBoolean = true;
        }
        
    }
    public Vector3 distanceRightCalculate(Vector3 vecs)
    {
        rightDistance = transform.position - vecs;
        if (rightDistance.x < 0)
        {
            rightDistance = rightDistance * -1;
        }
        return rightDistance;
    }
}
