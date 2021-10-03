using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.Utils;

public class ExecuteEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableEffect());
    }

    IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(Utils.GetAnimationClipDurantion("ExecutionEffect", GetComponent<Animator>()));
        gameObject.SetActive(false);
    }
}
