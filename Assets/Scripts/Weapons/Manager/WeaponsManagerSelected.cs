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
    public bool isRH;


    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        if (isPlayingDistance)
            MoveDistanceWeapon();
        else
            MoveCacWeapon();


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

            collision.transform.rotation = Quaternion.Euler(0, 0,0);
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
        GetReferencesAndSetUP(weapon);
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
    protected CacWeapons cacWeapons;
    protected DistanceWeapon distanceWeapons;
    protected SpriteRenderer spriteRenderer;

    Vector3 PositionArme = Vector3.zero;
    Vector3 PosAttackPoint = Vector3.zero;

    protected void MoveDistanceWeapon()
    {
        

        if (_weapons != null)
        {
            if (isPlayingCac && _weapons.isAttacking)
                return;

            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 playerDirection = (mousePosition - transform.position);
            

            Vector3 aimDirection = (mousePosition - _weapons.transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            //print(angle);
            _weapons.transform.eulerAngles = new Vector3(0, 0, angle);

            GetHand(playerDirection);

            if (playerDirection.y > 0)
            {
                SetWeaponYPositionAndLayer(ref _weapons, -1);
            }
            else if(playerDirection.y < 0)
            {
                SetWeaponYPositionAndLayer(ref _weapons,1);
            }
            
            if (playerDirection.x < 0f && !spriteRenderer.flipY)
            {
                spriteRenderer.flipY = true;
                PositionArme = new Vector3(-PositionArme.x, PositionArme.y);
                _weapons.transform.localPosition = PositionArme;
                _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, -PosAttackPoint.y);
            }
            else if (playerDirection.x >0 && spriteRenderer.flipY)
            {
                spriteRenderer.flipY = false;
                PositionArme = new Vector3(-PositionArme.x, PositionArme.y);
                _weapons.transform.localPosition = PositionArme;
                _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, PosAttackPoint.y);
            }
        }
    }

    protected void MoveCacWeapon()
    {
        if (_weapons != null)
        {
            
            if (_weapons.isAttacking)
                return;

            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 playerDirection = (mousePosition - transform.position);
            if (playerDirection.magnitude < 1f)
                return;
            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            var cacWeapons = (CacWeapons)_weapons;
            _weapons.attackPoint.position = transform.position + aimDirection * cacWeapons.attackRange;

            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _weapons.transform.eulerAngles = new Vector3(0, 0, angle);

            if (aimDirection.y > 0.6)
            {
                SetWeaponYPositionAndLayer(ref _weapons, -1);
            }
            else if (aimDirection.y < 0.5)
            {
                SetWeaponYPositionAndLayer(ref _weapons, 1);
            }
        }
    }


    private void GetReferencesAndSetUP(GameObject weapon)
    {
        _weapons = weapon.GetComponent<Weapons>();
        spriteRenderer = _weapons.GetComponent<SpriteRenderer>();
        PosAttackPoint = _weapons.attackPointPos;
        SetRightDistanceAttackPointPos();
        SetWeaponXPosition(_weapons);
    }
    void GetHand(Vector3 playerDirection)
    {
        if (playerDirection.x < 0f)
        {
            isRH = false;
        }
        else if (playerDirection.x > 0)
        {
            isRH = true;
        }
    }

    private void SetRightDistanceAttackPointPos()
    {
        if(_weapons is DistanceWeapon)
        {
            if (!spriteRenderer.flipY)
                _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, PosAttackPoint.y);
            else
                _weapons.attackPoint.localPosition = new Vector3(PosAttackPoint.x, -PosAttackPoint.y);
        }
       
    }

    private void SetWeaponXPosition( Weapons weapons)
    {
        PositionArme.x = weapons.offSetX;
        weapons.transform.localPosition = PositionArme;
    }

    private void SetWeaponYPositionAndLayer(ref Weapons _weapons, int orderInLayer)
    {
        PositionArme.y = _weapons.offSetY;
        _weapons.transform.localPosition = PositionArme;
        spriteRenderer.sortingOrder = orderInLayer;
    }
    #endregion

}
