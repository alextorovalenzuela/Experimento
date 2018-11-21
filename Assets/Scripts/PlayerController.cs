using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public struct Accion
{
	public string nombre;
	public bool estatico;
	public bool objetivoEsElMismo;

	public string mensaje;
	public int argumento;

	public string animacionTrigger;

	public int costoMana;
}
public class PlayerController : MonoBehaviour {

	// Use this for initialization
	public List<Accion> Acciones;

	public string nombre;
	public int vida;
	public int mana;
	public bool aliado;
	public bool sigueVivo;

	public float speed = 4f;
	Animator anim;
	Rigidbody2D rb2d;
	Vector2 mov;
	NavMeshAgent nv;
	ManagerPelea mp;

	void CambiarVida(int cant)
	{
		vida += cant;
		mp.ActualizarInterface();
	}
	void CambiarMana(int cant)
	{
		mana += cant;
		mp.ActualizarInterface();

	}
	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		//nv = GetComponent<NavMeshAgent>();

		//nv.updateRotation =false;
	}
	
	public IEnumerator EjecutarAccion(Accion accion, Transform objetivo){

		CambiarMana(-accion.costoMana);
		if(accion.objetivoEsElMismo)
			objetivo = transform;
		if(accion.estatico)
		{
			anim.SetTrigger(accion.animacionTrigger);
			objetivo.SendMessage(accion.mensaje, accion.argumento);
		}	
		else 
		{
				Vector2 PosInicial = transform.position;

				transform.LookAt(objetivo.transform.position);
				//nv.SetDestination(objetivo.position);

				while (Vector2.Distance(transform.position, objetivo.position) > 2)
					yield return null;
				
				anim.SetTrigger(accion.animacionTrigger);
				objetivo.SendMessage(accion.mensaje, accion.argumento);
			
				transform.LookAt(PosInicial);
				//nv.SetDestination(PosInicial);

				while (Vector2.Distance(transform.position, objetivo.position) > 0.1f)
					yield return null;
		
		}
	}
	// Update is called once per frame
	void Update () {
		
		mov = new Vector2(
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);
		
		   anim.SetFloat("movX", mov.x);
		   anim.SetFloat("movY", mov.y);

			if (mov != Vector2.zero){
				anim.SetFloat("movX", mov.x);
		    	anim.SetFloat("movY", mov.y);
				anim.SetBool("walking", true);
			} else {
				anim.SetBool("walking", false);
			}

			if(Input.GetKeyDown("space")){
				anim.SetTrigger("attacking");
			}
	}
	

	void  FixedUpdate() {
		rb2d.MovePosition(rb2d.position + mov *speed *Time.deltaTime);
	
	}
}
