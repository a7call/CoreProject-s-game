using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdung : MonoBehaviour
{
    public bool left = false;
    public bool up = false;
    public bool right = false;
    public bool down = false;
    void Start()
    {

        Invoke("PickSprite", 3f);
    }


    void PickSprite()
    { //picks correct sprite based on the four door bools

        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {

                        this.GetComponent<SpriteRenderer>().color = Color.blue;


                    }
                    else
                    {


                        GetComponent<SpriteRenderer>().color = Color.yellow;

                    }
                }
                else if (left)
                {

                    GetComponent<SpriteRenderer>().color = Color.blue;

                }
                else
                {

                    GetComponent<SpriteRenderer>().color = Color.cyan;

                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {

                        GetComponent<SpriteRenderer>().color = Color.black;

                    }
                    else
                    {

                        GetComponent<SpriteRenderer>().color = Color.magenta;

                    }
                }
                else if (left)
                {

                    GetComponent<SpriteRenderer>().color = Color.green;

                }
                else
                {


                    GetComponent<SpriteRenderer>().color = Color.grey;





                }
            }
            return;
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {

                    GetComponent<SpriteRenderer>().color = Color.green;

                }
                else
                {

                    GetComponent<SpriteRenderer>().color = Color.blue;

                }
            }
            else if (left)
            {

                GetComponent<SpriteRenderer>().color = Color.gray;

            }
            else
            {


                GetComponent<SpriteRenderer>().color = Color.green;



            }
            return;
        }
        if (right)
        {
            if (left)
            {

                GetComponent<SpriteRenderer>().color = Color.white;

            }
            else
            {


                GetComponent<SpriteRenderer>().color = Color.yellow;


            }
        }
        else
        {


            GetComponent<SpriteRenderer>().color = Color.blue;



        }
        return;
    }


}
