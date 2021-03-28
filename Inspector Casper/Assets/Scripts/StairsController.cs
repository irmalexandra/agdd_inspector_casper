using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlatformEffector2D effector;
    private bool _fixing = false;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    public void flip_effector()
    {
        effector.rotationalOffset += 180;
        if (!_fixing)
        {
            StartCoroutine(fix_platform());
        }
    }
    
    
    private IEnumerator fix_platform()
    {
        _fixing = true;
        Debug.Log(_fixing);
        yield return new WaitForSeconds(0.5f);
        effector.rotationalOffset -= 180;
        _fixing = false;
        Debug.Log(_fixing);

    }
}
