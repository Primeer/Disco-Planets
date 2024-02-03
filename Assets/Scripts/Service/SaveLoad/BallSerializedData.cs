using MemoryPack;
using UnityEngine;

namespace Service.SaveLoad
{
    [MemoryPackable]
    public partial class BallSerializedData
    {
        public Vector2 Position;
        public int Value;
    }
}