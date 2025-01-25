
using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Time;

    public void OnEnable()
    {
        StartCoroutine(KillCoroutine());
    }

    private IEnumerator KillCoroutine()
    {
        yield return new WaitForSeconds(Time);
        Destroy(gameObject);
    }
}
