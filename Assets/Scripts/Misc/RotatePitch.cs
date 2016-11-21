using UnityEngine;
//This script will run constantly to any object it is attached to
public class RotatePitch : MonoBehaviour
{
    //Update is called as often as possible
    void Update()
    {
        transform.Rotate(new Vector3(45, 0, 0) * Time.deltaTime);
    }
}
