using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Weapons : MonoBehaviour, IPunObservable
{
    public Transform firePoint;
    public Animator anim;
    public GameObject bullet_9mm;
    private AudioSource aud;

    public GameObject ammo_text;
    private Text info;
    public bool isPistol;
    public bool isShotgun;
    public int shotgun_ammo;
    public int shotgun_mag;
    public int pistol_ammo;
    public int pistol_mag;

    public PhotonView photonView;

    public bool havePistol;
    public bool haveShotgun;

    public AudioClip[] weapons_sounds;

    public GameObject Glock;
    public GameObject MP;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        ammo_text = GameObject.Find("ammo_text");
        info = ammo_text.GetComponent<Text>();
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        isPistol = true;
        isShotgun = false;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Equip();
            Gun();
        } 
    }

    public void Equip()
    {
        if (isPistol)
        {
            Glock.SetActive(true);
            anim.SetBool("isPistol", true);
            info.text = (pistol_mag + " / " + pistol_ammo);
            info.enabled = true;
        }
        else
        {
            Glock.SetActive(false);
            anim.SetBool("isPistol", false);
        }

        if (isShotgun)
        {
            MP.SetActive(true);
            anim.SetBool("isShotgun", true);
            info.text = (shotgun_mag + " / " + shotgun_ammo);
            info.enabled = true;
        }
        else
        {
            MP.SetActive(false);
            anim.SetBool("isShotgun", false);
        }
    }

    public void Reload_Shotgun()
    {
        if (shotgun_ammo != 0 && shotgun_mag < 4)
        {
            aud.PlayOneShot(weapons_sounds[5]);
            anim.SetTrigger("shotgun_reload");
            shotgun_ammo -= 1;
            shotgun_mag += 1;
        }
    }

    public void Reload_Pistol()
    {
        int reason = 8 - pistol_mag;
        if (pistol_ammo >= reason && reason != 0 && pistol_ammo != 0)
        {
            aud.PlayOneShot(weapons_sounds[2]);
            anim.SetTrigger("pistol_reload");
            pistol_ammo -= reason;
            pistol_mag = 8;
        }
        else if (pistol_ammo < reason && reason != 0 && pistol_ammo != 0)
        {
            aud.PlayOneShot(weapons_sounds[2]);
            anim.SetTrigger("pistol_reload");
            pistol_mag += pistol_ammo;
            pistol_ammo = 0;
        }
    }

    public void Shoot_S()
    {
        if (isPistol == true && anim.GetFloat("moveX") == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("pers_idle_pistol"))
        {
            if (pistol_mag < 0) { pistol_mag = 0; }
            if (pistol_mag != 0)
            {
                anim.SetBool("pistol_shoot", true);
                aud.PlayOneShot(weapons_sounds[0]);
                pistol_mag--;
                PhotonNetwork.Instantiate("bullet_9mm", firePoint.position, firePoint.rotation);
            }
            else
            {
                anim.SetBool("pistol_empty_shoot", true);
                aud.PlayOneShot(weapons_sounds[1]);
            }
        }

        if (isShotgun == true && anim.GetFloat("moveX") == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("pers_idle_shotgun"))
        {
            if (shotgun_mag < 0) { shotgun_mag = 0; }
            if (shotgun_mag != 0)
            {
                anim.SetBool("shotgun_shoot", true);
                aud.PlayOneShot(weapons_sounds[3]);
                shotgun_mag--;
                PhotonNetwork.Instantiate("bullet_9mm", firePoint.position, firePoint.rotation);
            }
            else
            {
                anim.SetBool("shotgun_empty_shoot", true);
                aud.PlayOneShot(weapons_sounds[4]);
            }
        }
    }

    public void Shoot_F()
    {
        if (isPistol == true && anim.GetFloat("moveX") == 0)
        {
            if (pistol_mag != -1)
            {
                anim.SetBool("pistol_shoot", false);
            }
            if (pistol_mag <= 0)
            {
                anim.SetBool("pistol_empty_shoot", false);
            }
        }

        if (isShotgun == true && anim.GetFloat("moveX") == 0)
        {
            if (shotgun_mag != -1)
            {
                anim.SetBool("shotgun_shoot", false);
            }
            if (shotgun_mag <= 0)
            {
                anim.SetBool("shotgun_empty_shoot", false);
            }
        }
    }


    private void Gun()
    {
        if(Input.GetKeyDown(KeyCode.I) && haveShotgun)
        {
            isShotgun = true;
            isPistol = false;
        }
        if(Input.GetKeyDown(KeyCode.U) && havePistol)
        {
            isPistol = true;
            isShotgun = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(MP.activeSelf);
            stream.SendNext(Glock.activeSelf);
        }
        else
        {
            MP.SetActive((bool)stream.ReceiveNext());
            Glock.SetActive((bool)stream.ReceiveNext());
        }
    }
}
