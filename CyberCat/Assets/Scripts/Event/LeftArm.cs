using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : MonoBehaviour
{
    private bool leftArmBool;
    public bool leftArm { get {return leftArmBool; } set { leftArmBool = value; } }
    public Vector3 leftDistance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameManager.Instance.LeftArmBoolean = true;
        }

    }

    public Vector3 distanceLeftCalculate(Vector3 vecs)
    {
        leftDistance = transform.position - vecs;
        if (leftDistance.x < 0)
        {
            leftDistance = leftDistance * -1;
        }
        return leftDistance;
    }
}