using UnityEngine;
//This script will run constantly to any object it is attached to
public class RotatePitch : MonoBehaviour
{
    //Update is called as often as possible
    void Update()
    {
		transform.RotateAround(transform.position, new Vector3(0,0,1),  45 * Time.deltaTime);
    }
}
