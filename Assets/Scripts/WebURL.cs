using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WebURL : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirWeb()
    {
        Application.OpenURL("www.atlassian.com/es/software/jira");
    }
}
