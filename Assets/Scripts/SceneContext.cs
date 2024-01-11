using UnityEngine;
using View;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private Transform ballsRoot;
    [SerializeField] private Transform throwerBallRoot;
    [SerializeField] private Transform effectsRoot;
    [SerializeField] private BallView mainBall;
        
    public Transform BallsRoot => ballsRoot;
    public Transform ThrowerBallRoot => throwerBallRoot;
    public Transform EffectsRoot => effectsRoot;
    public BallView MainBall => mainBall;
    public Vector2 GravitationPoint => mainBall.transform.position;
}