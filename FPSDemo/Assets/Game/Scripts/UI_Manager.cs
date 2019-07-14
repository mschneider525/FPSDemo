using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammoCount = null;

    [SerializeField]
    private GameObject _coin = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmo(int ammoCount)
    {
        _ammoCount.text = "Ammo: " + ammoCount;
    }

    public void AddCoinToInventory()
    {
        _coin.SetActive(true);
    }

    public void RemoveCoinFromInventory()
    {
        _coin.SetActive(false);
    }
}
