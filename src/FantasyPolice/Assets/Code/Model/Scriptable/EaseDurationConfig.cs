using DG.Tweening;
using Model.Data;
using UnityEngine;

namespace Model.Scriptable
{
    [CreateAssetMenu(fileName = "EaseDurationConfig", menuName = "FantasyPolice/Configs/Create EaseDurationConfig")]
    public class EaseDurationConfig : ScriptableObject, IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;

        public string Id => _id;
        public Ease Ease => _ease;
        public float Duration => _duration;
    }
}