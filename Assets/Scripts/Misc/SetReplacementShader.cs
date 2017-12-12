using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetReplacementShader : MonoBehaviour {
    
    public Shader s;
    
    void OnEnable () {
        GetComponent<Camera>().SetReplacementShader(s, "Minimap");
    }

    private void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
}
