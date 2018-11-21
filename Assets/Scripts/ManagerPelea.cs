using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPelea : MonoBehaviour {


	public List<PlayerController> players;

	public static ManagerPelea singleton;
	public Button prefab;
	public Text textoEstado;
	public Transform panel;
	// Use this for initialization
	void Awake () {
		if (singleton != null)
		{
			Destroy(gameObject);
			return;
		}
		singleton = this;
	}
	
	public void ActualizarInterface()
	{
		textoEstado.text="";
		foreach (var playerController in players)
		{
			if (playerController.sigueVivo)
			{
				textoEstado.text += "<color= " + (playerController.aliado ? "lime " : "red") + ">" +
				playerController.nombre + " HP: " +playerController.vida + "/100 MANA " + playerController.mana + "/100.</color>\n";
			}
		}
	}
	private void Start() 
	{
		ActualizarInterface();
		StartCoroutine("Bucle");	

	}


	List<Button> poolBotones = new List<Button>();
	IEnumerator Bucle ()
	{
		while (true)
		{
			foreach (var playerController in players)
			{
				IEnumerator c = null;

				for (int i = 0; i < poolBotones.Count; i++)
				{
					poolBotones[i].gameObject.SetActive(false);
				}
				if (playerController.aliado)
				{
					if (playerController.aliado)
					{
						foreach (var accion in playerController.Acciones)
						{
							Button b = null;
							for (int i = 0; i < poolBotones.Count; i++)
							{
								if (!poolBotones[i].gameObject.activeInHierarchy)
								{
									b = poolBotones[i];
								}
							}
							b = Instantiate(prefab, panel);

							b.transform.position = Vector2.zero;
							b.transform.localScale = Vector2.one;

							poolBotones.Add(b);

							b.gameObject.SetActive(true);
							b.onClick.RemoveAllListeners();
							b.GetComponentInChildren<Text>().text = accion.nombre;	
								
								if (playerController.mana < accion.costoMana)
								{
									b.interactable = false;
								}
								else
								{
									b.interactable = true;
									b.onClick.AddListener(() => {});

									for (int j = 0; j < poolBotones.Count; j++)
									{
										poolBotones[j].gameObject.SetActive(false);
									}

									c=playerController.EjecutarAccion(
									accion,
									players[Random.Range(0, players.Count)].transform);
								}
						}		
							
						

								
					}
				}
				else
				{
					c=playerController.EjecutarAccion(
						playerController.Acciones[Random.Range(0, playerController.Acciones.Count)],
						players[Random.Range(0, players.Count)].transform);
				}

				while (c == null)
				{
					yield return null;
				}
				StartCoroutine(c);
			}
		}
	}
	void Update () {
		
	}
}
