using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject panelJugador;
	public Button[] mainButtonArray = new Button[4];
	
	public bool inPanelJugador = true;

	public GameObject panelAtacar;
	public Button[] optionButtonAtacar = new Button[2];
	public bool inPanelAtacar = false;
	public GameObject panelHabilidad;
	public Button[] optionButtonHabilidad = new Button[2];
	public bool inPanelHabilidad = false;
	public int MousePosition=0;
	
	

	void Start()
	{

		
		Vector3 buttonPosition = mainButtonArray[MousePosition].transform.position;
		transform.position = new Vector3(buttonPosition.x - 120, buttonPosition.y, buttonPosition.z);
		
	}

	void Update()
	{
	
		if(Input.GetMouseButtonDown(0) && inPanelJugador)
		{
			switch (MousePosition)
			{
				/*Singleplayer */
				case 0:
					LoadPanelAtacar();
				break;
				/*BattleNet */
				case 1:
					Debug.Log("chao :(");
				break;
				/* Local Area Network */
				case 2:
					LoadPanelHabilidad();
				break;
				
				case 3:
					Quit();
				break;
				/* Credits */
				
			}
		}
		else if(Input.GetMouseButtonDown(0) && inPanelAtacar)
		{
			switch (MousePosition)
			{
				case 0:
					Debug.Log("hola :(");
					LoadPanelJugador();
				break;
				case 1:
					Debug.Log("hola :(");
					LoadPanelJugador();
				break;
								
			}
		}else if(Input.GetMouseButtonDown(0) && inPanelHabilidad)
		{
			switch (MousePosition)
			{
				/*Singleplayer */
				case 0:
					Debug.Log("Opcion no implementada :(");	
					LoadPanelJugador();
								break;
				/*BattleNet */
				case 1:
					Debug.Log("Opcion no implementada :(");
					LoadPanelJugador();
				break;
				/* Local Area Network */
				
			}
		}
	}
	private void MoveArrow(int movement, Button[] currentArray)
	{
		MousePosition += movement;
		if(MousePosition < 0)
			MousePosition = 0;
		if(MousePosition > currentArray.Length - 1)
			MousePosition = currentArray.Length - 1;
			
		Vector3 buttonPosition = currentArray[MousePosition].transform.position;
		transform.position = new Vector3(buttonPosition.x - 120, buttonPosition.y, buttonPosition.z);
	}

	public void PlayGame()
	{
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{
		Debug.Log("You just quited the game");
		Application.Quit();
	}

	

	public void LoadPanelJugador()
	{
		inPanelAtacar = false;
		inPanelHabilidad=false;
		inPanelJugador = true;
		
		panelAtacar.SetActive(false);
		panelHabilidad.SetActive(false);
		panelJugador.SetActive(true);

		

	}

	public void LoadPanelAtacar()
	{
		inPanelAtacar = true;
		inPanelHabilidad=false;
		inPanelJugador = false;
		
		
		panelAtacar.SetActive(true);
		panelHabilidad.SetActive(false);
		panelJugador.SetActive(false);

		
	}
	public void LoadPanelHabilidad()
	{
		inPanelAtacar = false;
		inPanelHabilidad=true;
		inPanelJugador = false;

		
		panelAtacar.SetActive(false);
		panelHabilidad.SetActive(true);
		panelJugador.SetActive(false);

		
	}
}