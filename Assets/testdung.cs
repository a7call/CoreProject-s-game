using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdung : MonoBehaviour
{
    public bool left = false;
    public bool up = false;
    public bool right = false;
    public bool down = false;
    
    //DOORS
    [SerializeField] GameObject DoorUpTileMap;
    [SerializeField] GameObject DoorDownTileMap;
    [SerializeField] GameObject DoorLeftTileMap;
    [SerializeField] GameObject DoorRightTileMap;


    // CORRIDORS
    [SerializeField] GameObject CorridorDownTileMap;
    [SerializeField] GameObject CorridorUpTileMap;
    [SerializeField] GameObject CorridorRightTileMap;
    [SerializeField] GameObject CorridorLeftTileMap;


    [SerializeField] GameObject DoorFaceSprite;
    [SerializeField] GameObject DoorOthersSprite;

   protected virtual void Start()
    {
        CorridorUpTileMap.SetActive(false); 
        CorridorDownTileMap.SetActive(false);
        CorridorRightTileMap.SetActive(false);
        CorridorLeftTileMap.SetActive(false);

        DoorFaceSprite.SetActive(false);
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


                        CorridorUpTileMap.SetActive(true);
                        CorridorDownTileMap.SetActive(true);
                        CorridorRightTileMap.SetActive(true);
                        CorridorLeftTileMap.SetActive(true);

                        DoorFaceSprite.SetActive(true);

                    }
                    else
                    {


                        Destroy(DoorUpTileMap);
                        Destroy(DoorDownTileMap);
                        Destroy(DoorRightTileMap);


                        CorridorUpTileMap.SetActive(true);
                        CorridorDownTileMap.SetActive(true);
                        CorridorRightTileMap.SetActive(true);

                        DoorFaceSprite.SetActive(true);

                    }
                }
                else if (left)
                {
                    Destroy(DoorUpTileMap);
                    Destroy(DoorDownTileMap);
                    Destroy(DoorLeftTileMap);


                    CorridorUpTileMap.SetActive(true);
                    CorridorDownTileMap.SetActive(true);
                    CorridorLeftTileMap.SetActive(true);

                    DoorFaceSprite.SetActive(true);

                }
                else
                {

                    Destroy(DoorUpTileMap);
                    Destroy(DoorDownTileMap);


                    CorridorUpTileMap.SetActive(true);
                    CorridorDownTileMap.SetActive(true);

                    DoorFaceSprite.SetActive(true);

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


                        CorridorUpTileMap.SetActive(true);
                        CorridorRightTileMap.SetActive(true);
                        CorridorLeftTileMap.SetActive(true);

                        DoorFaceSprite.SetActive(true);


                    }
                    else
                    {
                        Destroy(DoorUpTileMap);
                        Destroy(DoorRightTileMap);
                        CorridorRightTileMap.SetActive(true);
                        CorridorUpTileMap.SetActive(true);

                        DoorFaceSprite.SetActive(true);
                    }
                }
                else if (left)
                {
                    Destroy(DoorUpTileMap);
                    Destroy(DoorLeftTileMap);
                    CorridorLeftTileMap.SetActive(true);
                    CorridorUpTileMap.SetActive(true);

                    DoorFaceSprite.SetActive(true);

                }
                else
                {


                    Destroy(DoorUpTileMap);
                    CorridorUpTileMap.SetActive(true);
                    DoorFaceSprite.SetActive(true);
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
                    CorridorDownTileMap.SetActive(true);
                    CorridorRightTileMap.SetActive(true);
                    CorridorLeftTileMap.SetActive(true);
                    Destroy(DoorDownTileMap);
                    Destroy(DoorLeftTileMap);
                    Destroy(DoorRightTileMap);
                    

                }
                else
                {
                    CorridorDownTileMap.SetActive(true);
                    CorridorRightTileMap.SetActive(true);
                    Destroy(DoorDownTileMap);
                    Destroy(DoorRightTileMap);

                }
            }
            else if (left)
            {
                CorridorDownTileMap.SetActive(true);
                CorridorLeftTileMap.SetActive(true);
                Destroy(DoorDownTileMap);
                Destroy(DoorLeftTileMap);

            }
            else
            {
                CorridorDownTileMap.SetActive(true);

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
                CorridorRightTileMap.SetActive(true);
                CorridorLeftTileMap.SetActive(true);

            }
            else
            {

                CorridorRightTileMap.SetActive(true);
                Destroy(DoorRightTileMap);


            }
        }
        else
        {

            CorridorLeftTileMap.SetActive(true);
            Destroy(DoorLeftTileMap);



        }
        return;
    }


}
