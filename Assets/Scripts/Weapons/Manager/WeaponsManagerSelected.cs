using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wanderer.Utils;
/// <summary>
/// Weapon manager, gére le script actif en fontion de l'arme ramassé
/// </summary>
public class WeaponsManagerSelected : MonoBehaviour
{
    // On le met à moins 1 car la première arme ramassée correspond au 0 et non pas 1
    [HideInInspector]
    public int selectedCacWeapon = -1;
    [HideInInspector]
    public int selectedDistanceWeapon = -1;

    public List<GameObject> cacWeaponsList = new List<GameObject>();
    public List<GameObject> distanceWeaponsList = new List<GameObject>();

    [HideInInspector]
    public bool isPlayingCac=false;
    [HideInInspector]
    public bool isPlayingDistance=false;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        MoveWeapon();


        ChangeWeapons();
        UpdateUIWeapon();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CacWeapon") || collision.CompareTag("DistanceWeapon"))
        {
            collision.transform.parent = gameObject.transform;
            collision.GetComponent<Weapons>().enabled = true;
            Weapons _weapons = collision.GetComponent<Weapons>();
            collision.transform.localPosition = PositionArme;
            collision.transform.localRotation = Quaternion.Euler(0, 0, 0);
            collision.transform.gameObject.SetActive(false);
            collision.GetComponent<Collider2D>().enabled = false;
            
        

            if (transform.childCount == 1)
            {
                EquipeWeapon(transform.GetChild(0).gameObject);
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    UnEquipWeapon(transform.GetChild(i).gameObject);
                }
                EquipeWeapon(transform.GetChild(transform.childCount - 1).gameObject);

            }

            if (collision.CompareTag("CacWeapon"))
            {
                isPlayingCac = true;
                isPlayingDistance = false;
                selectedCacWeapon = transform.childCount - 1;
                cacWeaponsList.Add(collision.gameObject);
            }
            else
            {
                isPlayingDistance = true;
                isPlayingCac = false;
                selectedDistanceWeapon = transform.childCount - 1;
                distanceWeaponsList.Add(collision.gameObject);
            } 
        }
    }

    #region Select and equip Weapon

    GameObject previousDistanceWeap;
    GameObject previousCaCWeap;
    private void SelectWeapon()
    {
      
        int i = 0;
        int j = 0;
        if (isPlayingCac == true)
        {
            foreach (GameObject weapon in cacWeaponsList)
            {
                if (i == selectedCacWeapon)
                {
                    EquipeWeapon(weapon);
                }
                else
                {
                    UnEquipWeapon(weapon);
                }
                i++;
            }
            foreach(GameObject weapon in distanceWeaponsList)
            {
                UnEquipWeapon(weapon);
            }
        }

        if (isPlayingDistance == true)
        {
            foreach (GameObject weapon in distanceWeaponsList)
            {  
                if (j == selectedDistanceWeapon)
                {
                    EquipeWeapon(weapon);
                }
                else
                {
                    UnEquipWeapon(weapon);
                }
                j++;
            }
            foreach (GameObject weapon in cacWeaponsList)
            {
                UnEquipWeapon(weapon);
            }
        }
    }
    private void ChangeWeapons()
    {
        if (isPlayingCac == true)
        {
            int previousSelectedWeapon = selectedCacWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedCacWeapon >= cacWeaponsList.Count - 1)
                {
                    selectedCacWeapon = 0;
                }
                else
                {
                    selectedCacWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedCacWeapon <= 0)
                {
                    selectedCacWeapon = cacWeaponsList.Count - 1;
                }
                else
                {
                    selectedCacWeapon--;
                }
            }
            if (previousSelectedWeapon != selectedCacWeapon)
            {
                SelectWeapon();
            }
        }
        else if (isPlayingDistance == true)
        {
            int previousSelectedWeapon = selectedDistanceWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedDistanceWeapon >= distanceWeaponsList.Count - 1)
                {
                    selectedDistanceWeapon = 0;
                }
                else
                {
                    selectedDistanceWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedDistanceWeapon <= 0)
                {
                    selectedDistanceWeapon = distanceWeaponsList.Count - 1;
                }
                else
                {
                    selectedDistanceWeapon--;
                }
            }
            if (previousSelectedWeapon != selectedDistanceWeapon)
            {
                SelectWeapon();
            }
        }
    }

    private void EquipeWeapon(GameObject weapon)
    {
        weapon.gameObject.SetActive(true);
        GetReferences(weapon);
        SetUpWeaponForUse(_weapons);
        weapon.GetComponent<IPlayerWeapon>().WeaponData.Equip(transform.parent.GetComponent<Player>());
        if(weapon.GetComponent<CacWeapons>())
            previousCaCWeap = weapon;
        if (weapon.GetComponent<DistanceWeapon>())
            previousDistanceWeap = weapon;
    }

    private void UnEquipWeapon(GameObject weapon)
    {
        weapon.gameObject.SetActive(false);
        weapon.GetComponent<IPlayerWeapon>().WeaponData.Unequip(transform.parent.GetComponent<Player>());
    }
    #endregion


    #region Switch CAC to Distance mode
  

    public void SwitchAttackMode()
    {
        if (isPlayingCac)
        {
            if (previousDistanceWeap != null)
            {
                UnEquipWeapon(previousCaCWeap);
                isPlayingDistance = true;
                isPlayingCac = false;
                previousDistanceWeap.SetActive(true);

                EquipeWeapon(previousDistanceWeap);
                foreach (GameObject weapon in cacWeaponsList)
                {
                    weapon.SetActive(false);
                    
                }
            }
            else
            {
                return;
            }
               
        }
        else
        {
          
            if(previousCaCWeap != null)
            {
                UnEquipWeapon(previousDistanceWeap);
                isPlayingCac = true;
                isPlayingDistance = false;
                previousCaCWeap.SetActive(true);
                EquipeWeapon(previousCaCWeap);
                foreach (GameObject weapon in distanceWeaponsList)
                {
                    weapon.SetActive(false);
                    UnEquipWeapon(previousDistanceWeap);
                }
            }
            else
            {
                return;
            }
           
        }
      

    }
    #endregion


    #region UI
    public Sprite cacSprite;
    public Sprite distanceSprite;
    public string ammoText;
    public void UpdateUIWeapon()
    {
        for (int i = 0; i < cacWeaponsList.Count; i++)
        {
            if (i == selectedCacWeapon)
            {
                cacSprite = cacWeaponsList[i].GetComponent<Weapons>().image;
            }
        }
        for (int i = 0; i < distanceWeaponsList.Count; i++)
        {
            if (i == selectedDistanceWeapon)
            {
                distanceSprite = distanceWeaponsList[i].GetComponent<Weapons>().image;
                if(distanceWeaponsList[i].GetComponent<DistanceWeapon>())
                ammoText = distanceWeaponsList[i].GetComponent<DistanceWeapon>().BulletInMag.ToString();
            }
        }
    }
    #endregion


    #region Weapon rotation
    protected Weapons _weapons;
    protected SpriteRenderer spriteRenderer;

    Vector3 PositionArme = Vector3.zero;
    Vector3 PosAttackPoint = Vector3.zero;

    protected void MoveWeapon()
    {
        

        if (_weapons != null)
        {
            if (isPlayingCac && _weapons.isAttacking)
                return;

                Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 playerDirection = (mousePosition - transform.position);
            if (playerDirection.magnitude < 1f)
                return;
            Vector3 aimDirection = (mousePosition - _weapons.transform.position).normalized;

            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _weapons.transform.eulerAngles = new Vector3(0, 0, angle);

            if (aimDirection.y > 0.7)
            {
                SetWeaponYPositionAndLayer(ref _weapons, _weapons.topOffSetY, 0);
            }
            else if( aimDirection.y < 0.5)
            {
                SetWeaponYPositionAndLayer(ref _weapons, _weapons.otherOffsetY,1);
            }
            
            if (aimDirection.x < -0.5f && !spriteRenderer.flipY)
            {
                spriteRenderer.flipY = true;
                PositionArme = new Vector3(-PositionArme.x, PositionArme.y);
                _weapons.transform.localPosition = PositionArme;
                if(isPlayingDistance)
                    _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, -PosAttackPoint.y);
                else
                    _weapons.attackPoint.position =  transform.position + aimDirection;
                



            }
            else if (aimDirection.x > 0.7f && spriteRenderer.flipY)
            {
                spriteRenderer.flipY = false;
                PositionArme = new Vector3(-PositionArme.x, PositionArme.y);
                _weapons.transform.localPosition = PositionArme;
                if (isPlayingDistance)
                    _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, PosAttackPoint.y);
                else
                    _weapons.attackPoint.position = transform.position + aimDirection;
            }
        }
    }


    
    private void GetReferences(GameObject weapon)
    {
        _weapons = weapon.GetComponent<Weapons>();
        spriteRenderer = _weapons.GetComponent<SpriteRenderer>();
        PosAttackPoint = _weapons.attackPointPos;
        SetRightAttackPointPos();
    }

    private void SetRightAttackPointPos()
    {
        if (!spriteRenderer.flipY)
            _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, PosAttackPoint.y);
        else
            _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, -PosAttackPoint.y);
    }

    private void SetUpWeaponForUse(Weapons weapons)
    {
        
        GetWeapon(weapons);
        SetWeaponXPosition(weapons);
        
    }

    private void SetWeaponXPosition( Weapons weapons)
    {
        PositionArme.x = weapons.offSetX;
        weapons.transform.localPosition = PositionArme;
    }

    private void SetWeaponYPositionAndLayer(ref Weapons _weapons, float offSet, int orderInLayer)
    {
        PositionArme.y = offSet;
        _weapons.transform.localPosition = PositionArme;
        spriteRenderer.sortingOrder = orderInLayer;
    }

    private void GetWeapon(Weapons weapons)
    {
        if (isPlayingCac)
        {
           this._weapons = weapons;
        }
        else if (isPlayingDistance)
        {
           this._weapons = weapons;
        }
    }

    

    #endregion

    // Méthode générique non utilisée mais conservée [A utiliser potentiellement si le code ci dessus est trop lourd]
    //private void ScrollWeapons(int _selectedWeapon, List<GameObject> _selectedWeaponList)
    //{
    //    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    //    {
    //        if (_selectedWeapon >= _selectedWeaponList.Count - 1)
    //        {
    //            _selectedWeapon = 0;
    //        }
    //        else
    //        {
    //            _selectedWeapon++;
    //        }
    //    }

    //    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    //    {
    //        if (_selectedWeapon <= 0)
    //        {
    //            _selectedWeapon = _selectedWeaponList.Count - 1;
    //        }
    //        else
    //        {
    //            _selectedWeapon--;
    //        }
    //    }
    //}

    //private void WhichWeaponScroll()
    //{
    //    if (isPlayingCac == true)
    //    {
    //        print("Parcours liste cac");
    //        ScrollWeapons(selectedCacWeapon, cacWeaponsList);
    //    }
    //    else if (isPlayingDistance == true)
    //    {
    //        print("Parcours liste distance");
    //        ScrollWeapons(selectedDistanceWeapon, distanceWeaponsList);
    //    }
    //}

    /* Ancien methode MoveWeapon
   rotected Vector3 screenMousePos;
   protected Vector3 screenPlayerPos;
   protected Vector3 screenWeaponPos;
   private float angle;

   protected void MoveWeapon()
   {

       // position de la souris sur l'écran 
       screenMousePos = Input.mousePosition;
       Vector3 screenMousePos2 = Camera.main.ScreenToWorldPoint(screenMousePos);
       // position du player en pixel sur l'écran 
       screenPlayerPos = Camera.main.WorldToScreenPoint(transform.parent.transform.position);
       // position du point d'attaque 


       if (isPlayingCac)
       {

           _weapons = cacWeaponsList[selectedCacWeapon].GetComponent<Weapons>();

       }
       if (isPlayingDistance)
       {
           _weapons = distanceWeaponsList[selectedDistanceWeapon].GetComponent<Weapons>();

       }
       if (_weapons != null)
       {
           spriteRenderer = _weapons.GetComponent<SpriteRenderer>();


           Vector3 PositionArme = _weapons.OffPositionArme;
           Vector3 PosAttackPoint = _weapons.attackPoint.localPosition;


           Vector3 dir = new Vector3((screenMousePos - screenPlayerPos).x, (screenMousePos - screenPlayerPos).y);
          // print(_weapons.transform.position);

           if (dir.x < -5 && !spriteRenderer.flipX)
           {
               spriteRenderer.flipX = true;
               _weapons.transform.localPosition = new Vector3(-PositionArme.x, PositionArme.y);
               _weapons.attackPoint.localPosition = new Vector3(-PosAttackPoint.x, PosAttackPoint.y);

           }
           else if (dir.x > 5 && spriteRenderer.flipX)
           {
               spriteRenderer.flipX = false;
               _weapons.transform.localPosition = PositionArme;
               _weapons.attackPoint.localPosition = new Vector3(-PosAttackPoint.x, PosAttackPoint.y);

           }

           if (spriteRenderer.flipX)
           {
               angle = Quaternion.FromToRotation(Vector3.left, dir).eulerAngles.z;
           }
           else
           {
               angle = Quaternion.FromToRotation(Vector3.right, dir).eulerAngles.z;

           }

           if (Quaternion.Euler(0, 0, angle).z > 0.85)
           {
               angle = 100;
           }

           _weapons.transform.rotation = Quaternion.Euler(0, 0, angle);
       }

   }*/

}