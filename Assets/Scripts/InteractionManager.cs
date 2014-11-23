//----------------------------------------------------------------------
// @file InteractionManager.cs
//
// M�ster en Desarrollo de Videojuegos - Universidad Complutense de Madrid
// @date Mayo 2012
//----------------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// El manager es un helper que ayuda a localizar los objetos usables
/// m�s cercanos al GameObject que lo tiene como componente
/// El objeto en foco en cada momento ser� diferenciado gr�ficamente
/// </summary>
public class InteractionManager : MonoBehaviour
{
    #region Exposed Fields

    /// <summary>
    /// Frecuencia de refresco del manager
    /// </summary>
    public float m_TimeToUpdate = 0.2f;

    /// <summary>
    /// The angle of the sight cone on degrees.
    /// </summary>
    public float m_InteractionAngle = 90.0f; //Mathf.PI * 0.5f;

    /// <summary>
    /// Distancia de interacci�n (en metros)
    /// </summary>
    public float m_InteractionDistance = 3.0f;

    /// <summary>
    /// Matriz de transformaci�n del player (por eficiencia)
    /// </summary>
    public Transform m_Player = null;

    /// <summary>
    /// Lista de objetos cercanos interactuables
    /// <see cref="Stats"/>
    /// </summary>
    public List<GameObject> nearInteractiveObjs = new List<GameObject>();

    #endregion

    #region Internal Fields

    /// <summary>
    /// Lista de objetos interactuables
    /// <see cref="Stats"/>
    /// </summary>
    private List<GameObject> m_InteractiveObjs = new List<GameObject>();

    /// <summary>
    /// Tiempo transcurrido desde la �ltima actualizaci�n del manager
    /// </summary>
    private float m_ElapsedTime = 0.0f;

    /// <summary>
    /// Referencia al objeto interactuable m�s cercano
    /// </summary>
    private GameObject m_NearInteractiveObj = null;
    public GameObject NearInteractive { get { return m_NearInteractiveObj; } }


    #endregion

    #region MonoBehavior Called Methods

    /// <summary>
    /// Carga del script
    /// </summary>
    void Awake()
    {
        Creature Player = GameManager.Instance.Player;

        m_Player = Player.transform;

        _UpdateLists();


    }

    /// <summary>
    /// Actualizaci�n del manager
    /// </summary>
    void Update()
    {

        // Control de tiempos
        m_ElapsedTime += Time.deltaTime;
        if (m_ElapsedTime > m_TimeToUpdate)
        {
            _DoChecks(m_InteractiveObjs, out m_NearInteractiveObj);

            m_ElapsedTime = 0.0f;
        }
    }

    #endregion

    #region Check Methods

    /// <summary>
    /// Realiza todos los checks para los objetos interactuables
    /// </summary>
    /// <param name="objects">Lista de objetos interactuables</param>
    /// <param name="near">El objeto en foco</param>
    private void _DoChecks(List<GameObject> objects, out GameObject near)
    {
        float bestAngle = m_InteractionAngle;
        near = null;

        foreach (GameObject inter in objects)
        {
            // Obtenemos una referencia a la matriz de transformaci�n
            Stats interProp = inter.GetComponent<Stats>() as Stats;
            Transform interTransform = inter.transform;

            Vector3 dist;
            if (_CheckDistance(interTransform, interProp.m_InteractionRadius, m_Player, out dist))
            {
                float newAngle = 0.0f;
                //Debug.Log("Distance Passed");
                if ((interProp.m_InteractionAngle >= 360.0f) ||
                    _CheckInterCone(dist, interTransform, interProp.m_InteractionAngle * 0.5f))
                {
                    //rellenamos una lista con los objetos m�s cercanos al GameObject 
                    nearInteractiveObjs.Add(inter);

                    //Debug.Log("Cone Passed");
                    if (newAngle < bestAngle)
                    {
                        near = inter;
                        bestAngle = newAngle;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Comprueba si la distancia entre el objeto interactuable y el player est� dentro del rango
    /// </summary>
    /// <param name="interTransform">Matriz de transformaci�n del objeto</param>
    /// <param name="interDist">Distancia de interacci�n del objeto interactuable</param>
    /// <param name="m_Player">Matriz de transformaci�n del player</param>
    /// <param name="dist">Vector que va del jugador al �tem. Se usar� posteriormente</param>
    /// <returns>True si la comprobaci�n se cumple. False si no es as�</returns>
    private bool _CheckDistance(Transform interTransform, float interDist, Transform m_Player, out Vector3 dist)
    {
        // TODO 2 - Comprobaci�n de la distancia
        dist = interTransform.position - m_Player.transform.position;
        float sqrDist = (m_InteractionDistance + interDist) *
            (m_InteractionDistance + interDist);

        return dist.sqrMagnitude <= sqrDist;
    }

    /// <summary>
    /// Este m�todo comprueba si el player se encuentra dentro del cono de interacci�n del objeto
    /// </summary>
    /// <param name="dist">Vector que une el player con el �tem</param>
    /// <param name="interTransform">Matriz de transformaci�n del objeto interactivo</param>
    /// <param name="interAngle">�ngulo de interacci�n del objeto interactuable</param>
    /// <returns>True si la comprobaci�n se cumple. False si no es as�</returns>
    private bool _CheckInterCone(Vector3 dist, Transform interTransform, float interAngle)
    {
        // TODO 3 - Comprobaci�n del �ngulo
        //dist = Vector3.zero;
        float angle = Vector3.Angle(interTransform.forward, dist);

        return angle <= interAngle;
    }

    #endregion

    #region Interaction Objects Management Methods

    /// <summary>
    /// Recarga la lista de objetos usables
    /// </summary>
    private void _UpdateLists()
    {
        m_InteractiveObjs.Clear();

        Stats[] objects = FindObjectsOfType(typeof(Stats)) as Stats[];

        foreach (Stats prop in objects)
        {
            //if (prop.m_Usable)
            //{
            m_InteractiveObjs.Add(prop.gameObject);
            //}
        }

        Debug.Log("[" + m_InteractiveObjs.Count + "] interactive objs found in: " + Application.loadedLevelName);
    }

    /// <summary>
    /// A�ade un objeto usable
    /// </summary>
    /// <param name="interactiveObj">Gameobject que ser� a�adido al sistema</param>
    /// <returns>True si el GameObject se a�ade al sistema, falso en caso contrario</returns>
    public bool AddInteractiveObject(GameObject interactiveObj)
    {
        bool ret = false;

        Stats prop = interactiveObj.GetComponent<Stats>();
        if (prop && !IsInteractiveObject(interactiveObj) && (prop.m_Usable))
        {
            m_InteractiveObjs.Add(interactiveObj);
        }

        return ret;
    }

    /// <summary>
    /// Elimina un objeto de la lista de interactuables
    /// </summary>
    /// <param name="interactiveObj">Objeto que se eliminar� del sistema</param>
    /// <returns>True en caso de haber sido eliminado, falso en caso contrario</returns>
    public bool RemoveInteractiveObject(GameObject interactiveObj)
    {
        return m_InteractiveObjs.Remove(interactiveObj);
    }

    /// <summary>
    /// Indica si el GameObject pasado como par�metro est� en el manager
    /// </summary>
    /// <param name="interactiveObj">GameObject a comprobar</param>
    /// <returns>True si el GameObject ya estaba en el manager, false en caso contrario</returns>
    public bool IsInteractiveObject(GameObject interactiveObj)
    {
        bool found = false;
        for (int i = 0; i < m_InteractiveObjs.Count && !found; ++i)
            if (m_InteractiveObjs[i] == interactiveObj)
                found = true;

        return found;
    }

    #endregion


}

