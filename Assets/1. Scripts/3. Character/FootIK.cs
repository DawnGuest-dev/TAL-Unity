using UnityEngine;

public class FootIK : MonoBehaviour
{
    public bool IkActive= true;
    [Range(0f, 1f)]
    public float WeightPositionRight= 1f;
	[Range(0f, 1f)]
	public float WeightRotationRight= 0f;
    [Range(0f, 1f)]
    public float WeightPositionLeft = 1f;
	[Range(0f, 1f)]
	public float WeightRotationLeft = 0f;

    Animator anim;
    public Vector3 offsetFoot;
    public LayerMask RayMask;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    RaycastHit hit;

    void OnAnimatorIK(int _layerIndex)
    {
        if(IkActive)
        {
			Vector3 FootPos = anim.GetIKPosition(AvatarIKGoal.RightFoot);
            if (Physics.Raycast(FootPos + Vector3.up, Vector3.down, out hit, 1.2f, RayMask))
            {
				anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, WeightPositionRight);
				anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, WeightRotationRight);
				anim.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + offsetFoot);

                if (WeightRotationRight > 0f)
                {
                    Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0f);
                anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0f);
            }

			FootPos = anim.GetIKPosition(AvatarIKGoal.LeftFoot);
            if (Physics.Raycast(FootPos + Vector3.up, Vector3.down, out hit, 1.2f, RayMask))
            {
				anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, WeightPositionLeft);
				anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, WeightRotationLeft);
				anim.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + offsetFoot);

                if (WeightRotationLeft > 0f)
                {
                    Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, footRotation);
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0f);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0f);
            }
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0f);
        }

    }
}