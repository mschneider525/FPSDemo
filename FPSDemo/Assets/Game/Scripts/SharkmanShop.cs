using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkmanShop : MonoBehaviour
{
    //check for a collision
    //check for player
    //check for E key
    //if player has coin
    //remove coin from player
    //update inventory display
    //play win sound
    //else
    //debug.log, you have no coin!

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Action"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    if (player.hasCoin == true)
                    {
                        player.hasCoin = false;

                        UI_Manager uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
                        if (uiManager != null)
                        {
                            uiManager.RemoveCoinFromInventory();
                        }

                        AudioSource _purchaseAudioSource = this.GetComponent<AudioSource>();
                        _purchaseAudioSource.Play();

                        player.EnableWeapon();
                    }
                    else //if (player.hasCoin == false)
                    {
                        Debug.Log("You have no coin!");
                    } 
                }
            }
        }
    }
}
