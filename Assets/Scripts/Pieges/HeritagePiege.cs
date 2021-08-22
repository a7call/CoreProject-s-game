using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeritagePiege : MonoBehaviour
{
    #region Variables
    protected Animator animator;

    protected Player player;

    [SerializeField] protected string FireSound;

    #endregion Variables

    protected virtual void Awake()
    {
        GetReferences();
    }

    protected virtual void GetReferences()
    {
        player = FindObjectOfType<Player>();
        animator = gameObject.GetComponent<Animator>();
    }


    #region Sound

    #endregion
}
