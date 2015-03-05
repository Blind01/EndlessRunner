using UnityEngine;
using System.Collections;

public class BounceBehaviour : StateBehaviour {

    public float jumpForce = 3.0f;
    public float jumpCommandDelay = 0.5f;

    private float m_jumpDelay = 0.0f;

    public override void PerformAction(float axisAmount)
    {
        if (axisAmount > 0 && m_jumpDelay <= 0 && m_grounded)
        {
            m_parentController.rigidbody.AddForce(0.0f, jumpForce, 0.0f);
            m_jumpDelay = jumpCommandDelay;
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

    void FixedUpdate()
    {
        if (m_jumpDelay > 0.0f)
            m_jumpDelay -= Time.deltaTime;
    }
}
