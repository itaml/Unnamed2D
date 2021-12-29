using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float normalSpeed;
    private float speed;
    private bool faceRight = true;
    private Rigidbody2D rb;
    private Animator anim;
    public Weapons weapons_class;
    private GameObject take_text_obj;
    private Text take_text;
    private PhotonView photonView;
    public int HP;
    public GameObject currentObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        take_text_obj = GameObject.Find("take_text");
        take_text = take_text_obj.GetComponent<Text>();
    }

    private void Start()
    {
        HP = 100;
        speed = 0f;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        Walk();
        if (HP <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }
    }

    private void Walk() {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(speed));
        if (speed > 0 && !faceRight)
        { Flip(); }
        else if (speed < 0 && faceRight)
        { Flip(); }
    }

    public void OnLeftButton()
    {
        if(speed >= 0f)
        {
            speed = -normalSpeed;
        }
    }

    public void OnRightButton()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
        }
    }

    public void OnButtonUp()
    {
        speed = 0f;
    }


    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        if (collision.gameObject.tag == "MP")
        {
            take_text.enabled = true;
            currentObject = collision.gameObject;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            HP -= 20;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        if (collision.gameObject.tag == "MP")
        {
            take_text.enabled = false;
        }
    }

}
