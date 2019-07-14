using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] //Use AudioClip instead of AudioSource if the object gets destroyed right after a sound is supposed to play
    private AudioClip _coinPickUpAudioClip = null;


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
                    player.hasCoin = true;
                }

                //Use Camera.main.transform.position if sound is too low
                AudioSource.PlayClipAtPoint(_coinPickUpAudioClip, this.transform.position, 1.0f);
                Destroy(this.gameObject);

                UI_Manager uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
                if (uiManager != null)
                {
                    uiManager.AddCoinToInventory(); 
                }
            }
        }
    }

}
