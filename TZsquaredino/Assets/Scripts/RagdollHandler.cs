using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{

    private List<Rigidbody> _rigibodies;

    public void Initialize()
    {
        _rigibodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        Disable();
    }

    public void Enable()
    {
        foreach(Rigidbody rigidbody in _rigibodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    public void Disable()
    {
        foreach (Rigidbody rigidbody in _rigibodies)
        {
            rigidbody.isKinematic = true;
        }
    }

}
