using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    public int end;
    public static EndController Instance;
    void Awake()
    {
        if (EndController.Instance == null)
        {
            EndController.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEnd(int i)
    {
        end = i;
    }

    public int GetEnd()
    {
        return end;
    }

}
