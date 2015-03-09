using UnityEngine;
using System.Collections;

public class ShrinkBehaviour : StateBehaviour {

    public float shrinkHeight;
    public float shrinkTime;

    private CapsuleCollider m_heightCollider;
    private float m_initialColliderHeight;
    private Vector3 m_initialMeshScale;
    private Vector3 m_shrinkMeshScale;
    private bool m_isShrunk;
    private bool m_ExitingState;

    protected override void Setup()
    {
        Transform colliderTransform = m_parentController.transform.Find("yCollider");
        m_heightCollider = colliderTransform.GetComponent<CapsuleCollider>();
        m_initialColliderHeight = m_heightCollider.height;
        m_initialMeshScale = transform.localScale;
        m_shrinkMeshScale = m_initialMeshScale;
        m_shrinkMeshScale.y = m_shrinkMeshScale.y * (shrinkHeight / m_initialColliderHeight);
    }

    public override void PerformAction(float axisAmount)
    {
        if (!m_ExitingState && axisAmount == 1 && !m_isShrunk)
        {
            m_isShrunk = !m_isShrunk;
            StartCoroutine(MeshTransition(shrinkTime));
            StartCoroutine(ColliderTransition(shrinkTime));
        }
        else if (!m_ExitingState && axisAmount != 1 && m_isShrunk)
        {
            m_isShrunk = !m_isShrunk;
            StartCoroutine(MeshTransition(shrinkTime));
            StartCoroutine(ColliderTransition(shrinkTime));
        }
        else if (m_ExitingState)
        {
            m_isShrunk = false;
            StartCoroutine(ColliderTransition(m_parentController.transitionTime/2));
        }
    }

    protected override Vector3 GroundMovementForce(float axisAmount)
    {
        return new Vector3(groundControlForce * axisAmount, 0, 0);
    }

    protected override Vector3 AirMovementForce(float axisAmount)
    {
        return new Vector3(airControlForce * axisAmount, 0, 0);
    }

    public override void ExitState()
    {
        m_ExitingState = true;
    }

    public override void EnterState()
    {
        m_ExitingState = false;
    }

    private IEnumerator MeshTransition(float shrinkTimeScale)
    {
        Vector3 currentMeshScale = transform.localScale;
        for(float time = 0.0f; time <= shrinkTime; time += Time.deltaTime)
        {
            float lerpScale = time / shrinkTimeScale;
            if (m_isShrunk)
                transform.localScale = Vector3.Lerp(
                    currentMeshScale, m_shrinkMeshScale, lerpScale);
            else
                transform.localScale = Vector3.Lerp(
                    currentMeshScale, m_initialMeshScale, lerpScale);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    private IEnumerator ColliderTransition(float shrinkTimeScale)
    {
        float currentColliderHeight = m_heightCollider.height;
        for(float time = 0.0f; time <= shrinkTime; time += Time.deltaTime)
        {
            float lerpScale = time / shrinkTimeScale;
            if (m_isShrunk)
                m_heightCollider.height = Mathf.Lerp(
                    currentColliderHeight, shrinkHeight, lerpScale);
            else
                m_heightCollider.height = Mathf.Lerp(
                    currentColliderHeight, m_initialColliderHeight, lerpScale);
            yield return new WaitForFixedUpdate();
        }
        if (!m_isShrunk)
            m_heightCollider.height = m_initialColliderHeight;
        yield return null;
    }
}
