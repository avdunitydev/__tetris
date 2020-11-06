
using UnityEngine;

namespace AVDTetris
{
    class Block_Z : BaseBlock
    {
        readonly int[,] prototype = new int[,] {
            { 1, 1, 0 },
            { 0, 1, 1 },
            { 0, 0, 0 }};

        public Block_Z(GameObject prefab) : base(E_BlockType.Z, prefab) { }

        protected override int[,] GetProtoMatrix() => prototype;

    }
}
