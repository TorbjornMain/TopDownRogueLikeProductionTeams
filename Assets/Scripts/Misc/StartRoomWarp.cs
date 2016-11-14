using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartRoomWarp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadSceneAsync("James Test (2)", LoadSceneMode.Single);
        }
    }
}
