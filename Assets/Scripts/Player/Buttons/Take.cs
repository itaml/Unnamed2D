using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class Take : MonoBehaviour, IPointerClickHandler
{
	public Player player_class;
	private PhotonView PV;
	public Weapons weapons_class;

	private GameObject player;

	private void Start()
    {
		PV = GetComponent<PhotonView>();
    }
	private void Update()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		weapons_class = player.GetComponent<Weapons>();
		player_class = player.GetComponent<Player>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (player_class.currentObject.name == "9mm")
		{
			weapons_class.pistol_ammo += 8;
			Destroy(player_class.currentObject);
		}
		if (player_class.currentObject.CompareTag("MP"))
		{
			weapons_class.isShotgun = true; weapons_class.isPistol = false;
			weapons_class.haveShotgun = true;
			PhotonNetwork.Destroy(player_class.currentObject);
		}
		if (player_class.currentObject.name == "Glock")
		{
			weapons_class.isPistol = true; weapons_class.isShotgun = false;
			weapons_class.havePistol = true;
			Destroy(player_class.currentObject);
		}
	}
}