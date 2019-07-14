using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController = null;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;

    [SerializeField]
    private ParticleSystem _muzzleFlash = null;
    [SerializeField]
    private GameObject _muzzleFlashGO = null;
    [SerializeField]
    private bool _useMuzzleFlashparticleSystemToTurnMuzzleFlashOnAndOff = true;

    [SerializeField]
    private GameObject _hitmarkerPrefab = null;

    [SerializeField]
    private AudioSource _weaponAudio = null;

    [SerializeField]
    private int currentAmmoCount = 0;
    private int maxAmmoCount = 100;

    private bool _isReloading = false;

    private UI_Manager _uiManager = null;

    public bool hasCoin = false;

    [SerializeField]
    private GameObject _weapon = null;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        ToggleShoot();

        //Reload
        if ((Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Reload")) && _isReloading == false && currentAmmoCount != maxAmmoCount && _weapon.activeSelf == true)
        {
            //reload, which takes 1.5 seconds
            StartCoroutine(Reload_Routine());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorVisibility();
        }
    }

    void Move()
    {
        float xVelocity = Input.GetAxis("Horizontal") * _speed;
        float zVelocity = Input.GetAxis("Vertical") * _speed;

        Vector3 moveVelocity = new Vector3(xVelocity, 0, zVelocity);
        moveVelocity.y -= _gravity;

        //Transforms direction (velocity) from local space to world space
        moveVelocity = transform.TransformDirection(moveVelocity);

        _characterController.Move(moveVelocity * Time.deltaTime);
    }

    void Shoot()
    {
        currentAmmoCount--;
        _uiManager.UpdateAmmo(currentAmmoCount);

        //Viewport width and height goes from 0 to 1, so zenter is 0.5
        Vector3 viewPortCenter = new Vector3(0.5f, 0.5f, 0.0f);
        Ray rayOrigin = Camera.main.ViewportPointToRay(viewPortCenter);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo, 100.0f))
        {
            //Type Casting: (Don't have to do this in this case, but typecast to change to a different (matching) data type)
            //GameObject hitmarkerClone = Instantiate(_hitmarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            //GameObject hitmarkerClone = (GameObject)Instantiate(_hitmarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            if (hitInfo.transform.name != "Sharkman")
            {
                GameObject hitmarkerClone = Instantiate(_hitmarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitmarkerClone, 1.0f); 
            }

            Destructible crate = hitInfo.transform.GetComponent<Destructible>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    void ToggleShoot()
    {
        if ((Input.GetMouseButton(0) || Input.GetButton("Shoot")) && currentAmmoCount > 0 && _weapon.activeSelf == true)
        {
            Shoot();

            if (_useMuzzleFlashparticleSystemToTurnMuzzleFlashOnAndOff == true)
            {
                _muzzleFlash.Play();
            }
            else
            {
                _muzzleFlashGO.SetActive(true);
            }

            if (_weaponAudio.isPlaying == false)
            {
                _weaponAudio.Play();
            }
        }
        else
        {
            if (_useMuzzleFlashparticleSystemToTurnMuzzleFlashOnAndOff == true)
            {
                _muzzleFlash.Stop();
            }
            else
            {
                _muzzleFlashGO.SetActive(false);
            }

            _weaponAudio.Stop();
        }
    }

    //This doesn't quite work how I'd expect...
    void ToggleCursorVisibility()
    {
        if (Cursor.visible == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator Reload_Routine()
    {
        _isReloading = true;

        yield return new WaitForSeconds(1.5f);

        currentAmmoCount = maxAmmoCount;
        _uiManager.UpdateAmmo(currentAmmoCount);
        _isReloading = false;
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
    }
}
