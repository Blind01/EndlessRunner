using UnityEngine;
using System.Collections;

public class PlayerStateManager: MonoBehaviour 
{
    public StateBehaviour[] stateAttributes;
    public int startState;
    public float transitionTime;
    public Transform startPosition;
    public string horizontalMoveAxis;
    public string stateActionAxis;

    private int m_currentCollidersCount;
    private bool m_inTransition;
    private int m_currentState;
    private Vector3[] m_savedMeshScales;
    private StateBehaviour[] m_behaviours;

	// Use this for initialization
	void Start ()
    {
        startPosition = GameObject.FindGameObjectWithTag("InitialObject").transform;
        m_currentCollidersCount = 0;
        m_inTransition = false;
        transform.position = startPosition.position;
        m_savedMeshScales = new Vector3[stateAttributes.Length];
        m_behaviours = new StateBehaviour[stateAttributes.Length];
        for (int i = 0; i < stateAttributes.Length; i++)
        {
            GameObject go = (GameObject)Instantiate(stateAttributes[i].gameObject,
                transform.position, stateAttributes[i].transform.rotation);
            m_behaviours[i] = go.GetComponent<StateBehaviour>();
            m_behaviours[i].SetParentController(this);
            m_savedMeshScales[i] = go.transform.localScale;
            go.transform.parent = transform;
            go.SetActive(false);
        }
        m_currentState = (startState + 1) % stateAttributes.Length;
        SetState(startState);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        // make any requested transitions.
        if(!m_inTransition)
        {
            for (int i = 0; i < stateAttributes.Length; i++)
            {
                if(Input.GetAxis(stateAttributes[i].SelectStateInputCommand) == 1)
                    StartCoroutine(TransitionState(i));
            }
        }

        // if action key is pressed perform specific state action.
        m_behaviours[m_currentState].PerformAction(Input.GetAxis(stateActionAxis));

        // if move is requested make appropriate move based on if we are in contact with anything
        if(m_currentCollidersCount != 0)
            m_behaviours[m_currentState].horizontalGroundMove(Input.GetAxis(horizontalMoveAxis));
        else
            m_behaviours[m_currentState].horisontalAirMove(Input.GetAxis(horizontalMoveAxis));
	}

    void OnCollisionEnter(Collision col)
    {
        m_currentCollidersCount++;
    }

    void OnCollisionExit(Collision col)
    {
        m_currentCollidersCount--;
    }

    private IEnumerator TransitionState(int state)
    {
        if (state != m_currentState)
        {
            m_inTransition = true;
            m_behaviours[m_currentState].ExitState();
            m_behaviours[state].gameObject.SetActive(true);
            Vector3 currentMeshScale = m_behaviours[m_currentState].transform.localScale;
            for (float time = 0.0f; time <= transitionTime; time += Time.deltaTime)
            {
                float lerpScale = time / transitionTime;
                m_behaviours[m_currentState].transform.localScale = Vector3.Lerp(
                    currentMeshScale, new Vector3(0.0f, 0.0f, 0.0f), lerpScale);
                m_behaviours[state].transform.localScale = Vector3.Lerp(
                    new Vector3(0.0f, 0.0f, 0.0f), m_savedMeshScales[state], lerpScale);
                yield return new WaitForFixedUpdate();
            }
            SetState(state);
            m_behaviours[m_currentState].EnterState();
            m_inTransition = false;
        }
        yield return null;
    }

    private void SetState(int state)
    {
        m_behaviours[m_currentState].gameObject.SetActive(false);
        m_currentState = state;
        m_behaviours[m_currentState].gameObject.SetActive(true);
        rigidbody.mass = m_behaviours[m_currentState].mass;
        rigidbody.drag = m_behaviours[m_currentState].drag;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].material = m_behaviours[m_currentState].material;
        }
    }
}
