using UnityEngine;
using System.Collections;

public class SprintBehaviour : StateBehaviour {

    public float sprintBoostForce;

    private float m_sprintModifier;

    public override void PerformAction(float axisAmount)
    {
        if(axisAmount > 0)
        {
            m_sprintModifier = sprintBoostForce;
        }
        else
        {
            m_sprintModifier = 1;
        }
    }

    protected override Vector3 GroundMovementForce(float axisAmount)
    {
        return new Vector3(groundControlForce * axisAmount * m_sprintModifier, 0, 0);
    }

    protected override Vector3 AirMovementForce(float axisAmount)
    {
        return new Vector3(airControlForce * axisAmount * m_sprintModifier, 0, 0);
    }

}
