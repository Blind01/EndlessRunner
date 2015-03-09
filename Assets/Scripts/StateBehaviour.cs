using UnityEngine;
using System.Collections;

public abstract class StateBehaviour : MonoBehaviour
{
    public float mass = 1.0f;
    public float drag = 0.05f;
    public float maxGroundVelocity = 5.0f;
    public float maxAirVelocity = 5.0f;
    public float airControlForce = 3.0f;
    public float groundControlForce = 10.0f;
    public bool hasMaxFallVelocity = false;
    public float maxFallVelocity = -2;
    public string SelectStateInputCommand;
    public PhysicMaterial material;

    protected bool m_grounded;
    protected PlayerStateManager m_parentController;
   

    public void SetParentController(PlayerStateManager parentController)
    {
        m_parentController = parentController;
        Setup();
    }

    public abstract void PerformAction(float axisAmount);
    protected abstract Vector3 GroundMovementForce(float axisAmount);
    protected abstract Vector3 AirMovementForce(float axisAmount);
    public virtual void ExitState() {}
    public virtual void EnterState() {}
    protected virtual void Setup() {}

    public void horizontalGroundMove(float axisAmount)
    {
        m_grounded = true;
        if (m_parentController != null)
        {
            m_parentController.rigidbody.AddForce(GroundMovementForce(axisAmount));
            if(m_parentController.rigidbody.velocity.x > maxGroundVelocity)
            {
                Vector3 moveVelocity = m_parentController.rigidbody.velocity;
                moveVelocity.x = maxGroundVelocity;
                m_parentController.rigidbody.velocity = moveVelocity;
            }
            else if(m_parentController.rigidbody.velocity.x < -maxGroundVelocity)
            {
                Vector3 moveVelocity = m_parentController.rigidbody.velocity;
                moveVelocity.x = -maxGroundVelocity;
                m_parentController.rigidbody.velocity = moveVelocity;
            }
        }
    }

    public void horisontalAirMove(float axisAmount)
    {
        m_grounded = false;
        if(m_parentController != null)
        {
            m_parentController.rigidbody.AddForce(AirMovementForce(axisAmount));
        }
        if(hasMaxFallVelocity)
        {
            if (m_parentController.rigidbody.velocity.y < maxFallVelocity)
            {
                Vector3 fallVelocity = m_parentController.rigidbody.velocity;
                fallVelocity.y = maxFallVelocity;
                m_parentController.rigidbody.velocity = fallVelocity;
            }
        }

    }
}
