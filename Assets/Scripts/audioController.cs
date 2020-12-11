using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public List<AudioClip> clipList;
    public AudioClip hurtSFX, bossHurtSFX, ghostSFX,
    explosionSmallSFX, explosionBigSFX,
    attackMouse,attackTub,attackDog,attackFeed;
    private AudioSource audioSource,audioOneShot;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        audioOneShot = GameObject.Find("AudioOneShot").GetComponent<AudioSource>();
        // audioSource = GameObject.Find("")
    }

    public void playerHurtSFX(){
        audioOneShot.PlayOneShot(hurtSFX);
    }
    public void playerAttackMonSFX(string name){
        string monster="";
        if (name.Contains("mouse")){
            monster = "mouse";
        }else if (name.Contains("enemy")){
            monster = "dog";
        }else if (name.Contains("tub")){
            monster = "tub";
        }else if (name.Contains("feed")){
            monster = "feed";
        }
        switch (monster)
        {
            case "mouse":
                audioOneShot.PlayOneShot(attackMouse);
                break;
            case "dog":
                audioOneShot.PlayOneShot(attackDog);
                break;
            case "tub":
                audioOneShot.PlayOneShot(attackTub);
                break;
            case "feed":
                audioOneShot.PlayOneShot(attackFeed);
                break;
            default:
                break;
        }
    }
    public void playExplosionBigSFX(){
        audioOneShot.PlayOneShot(explosionBigSFX);
    }
    public void playExplosionSmallSFX(){
        audioOneShot.PlayOneShot(explosionSmallSFX);
    }
    public void playAttackBossSFX(){
        audioOneShot.PlayOneShot(bossHurtSFX);
    }
    
    public void playGhostSFX(){
        audioOneShot.clip = ghostSFX;
    }
    public void stopPlayGhostSFX(){
        audioOneShot.clip = null;
    }

}
