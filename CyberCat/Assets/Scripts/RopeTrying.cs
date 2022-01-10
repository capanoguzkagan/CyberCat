using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTrying : MonoBehaviour
{
    public GameObject enemiTransfrom;
    public GameObject cubes;
    Vector3 distance;
    float xDis = 0.35f;
    float yDis = 0.25f;
    float xLoc;
    GameObject[] deneme;

    public void RopeMethod()
    {
        this.transform.LookAt(enemiTransfrom.transform);
        distance = enemiTransfrom.transform.position - this.transform.position;
        //Debug.Log(distance);
        xLoc = distance.x / xDis;
        if (xLoc < 0)
        {
            xLoc = -xLoc;
        }
        var xLoc2 = (int)xLoc;
        deneme = new GameObject[xLoc2];
        for (int i = 0; i <= xLoc2 - 1; i++)
        {
            deneme[i] = Instantiate(cubes, Vector3.zero, Quaternion.identity);
            deneme[i].transform.SetParent(this.gameObject.transform);
            deneme[i].transform.localPosition = new Vector3(0, 0,0);
            deneme[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (i >= 1)
            {
                deneme[i].transform.localPosition = new Vector3(0, 0, deneme[i - 1].transform.localPosition.z + 0.35f);
            }
        }
        distance = enemiTransfrom.transform.position - this.transform.position;
        Debug.Log(distance);
    }
}
