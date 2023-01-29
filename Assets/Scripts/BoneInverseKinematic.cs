using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneInverseKinematic : MonoBehaviour
{
    public int ChainLeght;
    public Transform Target, Pole;

    public int Iterations = 10;
    public float Delta = 0.001f;
    [Range(0, 1)]
    public float SnapBackStrenght = 1.0f;

    protected float[] BonesLenght;
    protected float CompleteLenght;
    protected Transform[] Bones;
    protected Vector3[] Positions;

    protected Vector3[] StartDirectionSucc;
    protected Quaternion[] StartRotationBone;
    protected Quaternion StartRotationTarget;
    protected Quaternion StartRotationRoot;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Bones = new Transform[ChainLeght + 1];
        Positions = new Vector3[ChainLeght + 1];
        BonesLenght = new float[ChainLeght];

        StartDirectionSucc = new Vector3[ChainLeght + 1];
        StartRotationBone = new Quaternion[ChainLeght + 1];
        StartRotationTarget = Target.rotation;

        CompleteLenght = 0;

        Transform current = transform;
        for (int i = Bones.Length - 1; i >=0; i--)
        {
            Bones[i] = current;
            StartRotationBone[i] = current.rotation;
            if (i == Bones.Length - 1)
            {
                StartDirectionSucc[i] = Target.position - current.position;
            }
            else
            {
                StartDirectionSucc[i] = Bones[i+1].position- current.position;
                BonesLenght[i] = (Bones[i + 1].position - current.position).magnitude;
                CompleteLenght += BonesLenght[i];
            }
            current = current.parent;
        }
    }
    private void LateUpdate()
    {
        ResolveInverseKinematic();
    }
    private void ResolveInverseKinematic()
    {
        if (Target == null)
        {
            return;
        }
        if (BonesLenght.Length != ChainLeght)
        {
            Init();
        }

        for (int i = 0; i < Bones.Length; i++)
        {
            Positions[i] = Bones[i].position;
        }

        Quaternion RootRot = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
        Quaternion RootRotDiff = RootRot * Quaternion.Inverse(StartRotationRoot);

        if ((Target.position - Bones[0].position).sqrMagnitude >= CompleteLenght * CompleteLenght )
        {
            Vector3 direction = (Target.position - Positions[0]).normalized;
            for (int i = 1; i < Positions.Length; i++)
            {
                Positions[i] = Positions[i - 1] + direction * BonesLenght[i - 1];
            }
        }
        else
        {
            for (int i = 0; i < Positions.Length - 1; i++)
            {
                Positions[i + 1] = Vector3.Lerp(Positions[i + 1], Positions[i] + RootRotDiff * StartDirectionSucc[i], SnapBackStrenght);
            }
            for (int iteration = 0; iteration < Iterations; iteration++)
            {
                for (int i = Positions.Length - 1; i > 0; i--)
                {
                    if (i == Positions.Length - 1)
                    {
                        Positions[i] = Target.position;
                    }
                    else
                    {
                        Positions[i] = Positions[i+1] + (Positions[i] - Positions[i + 1]).normalized * BonesLenght[i];
                    }
                }
                for (int i = 1; i < Positions.Length; i++)
                {
                    Positions[i] = Positions[i - 1] + (Positions[i] - Positions[i - 1]).normalized * BonesLenght[i - 1];
                }

                if ((Positions[Positions.Length - 1] - Target.position).sqrMagnitude > Delta * Delta)
                {
                    break;
                }
            }
        }

        if (Pole != null)
        {
            for (int i = 1; i < Positions.Length - 1; i++)
            {
                Plane plane = new Plane(Positions[i + 1] - Positions[i - 1], Positions[i - 1]);
                Vector3 projectedPole = plane.ClosestPointOnPlane(Pole.position);
                Vector3 projectedBone = plane.ClosestPointOnPlane(Positions[i]);
                float angle = Vector3.SignedAngle(projectedBone - Positions[i - 1], projectedPole - Positions[i - 1], plane.normal);
                Positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (Positions[i] - Positions[i-1]) + Positions[i - 1];

            }
        }

        for (int i = 0; i < Positions.Length; i++)
        {
            if (i == Positions.Length - 1)
            {
                Bones[i].rotation = Target.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationBone[i];
            }
            else
            {
                Bones[i].rotation = Quaternion.FromToRotation(StartDirectionSucc[i], Positions[i+1] - Positions[i]) * StartRotationBone[i];
            }
            Bones[i].position = Positions[i];
        }
    }
}
