using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdung : MonoBehaviour
{
    public bool left = false;
    public bool up = false;
    public bool right = false;
    public bool down = false;

    [SerializeField] GameObject DoorUpTileMap;
    [SerializeField] GameObject DoorDownTileMap;
    [SerializeField] GameObject DoorLeftTileMap;
    [SerializeField] GameObject DoorRightTileMap;
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

                        Destroy(DoorUpTileMap);
                        Destroy(DoorDownTileMap);
                        Destroy(DoorLeftTileMap);
                        Destroy(DoorRightTileMap);

                    }
                    else
                    {


                        Destroy(DoorUpTileMap);
                        Destroy(DoorDownTileMap);
                        Destroy(DoorRightTileMap);

                    }
                }
                else if (left)
                {
                    Destroy(DoorUpTileMap);
                    Destroy(DoorDownTileMap);
                    Destroy(DoorLeftTileMap);

                }
                else
                {

                    Destroy(DoorUpTileMap);
                    Destroy(DoorDownTileMap);

                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        Destroy(DoorUpTileMap);
                        Destroy(DoorLeftTileMap);
                        Destroy(DoorRightTileMap);

                    }
                    else
                    {
                        Destroy(DoorUpTileMap);
                        Destroy(DoorRightTileMap);

                    }
                }
                else if (left)
                {
                    Destroy(DoorUpTileMap);
                    Destroy(DoorLeftTileMap);

                }
                else
                {


                    Destroy(DoorUpTileMap);
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

                    Destroy(DoorDownTileMap);
                    Destroy(DoorLeftTileMap);
                    Destroy(DoorRightTileMap);

                }
                else
                {

                    Destroy(DoorDownTileMap);
                    Destroy(DoorRightTileMap);

                }
            }
            else if (left)
            {

                Destroy(DoorDownTileMap);
                Destroy(DoorLeftTileMap);

            }
            else
            {


                Destroy(DoorDownTileMap);



            }
            return;
        }
        if (right)
        {
            if (left)
            {

                Destroy(DoorLeftTileMap);
                Destroy(DoorRightTileMap);

            }
            else
            {


                Destroy(DoorRightTileMap);


            }
        }
        else
        {


            Destroy(DoorLeftTileMap);



        }
        return;
    }


}
