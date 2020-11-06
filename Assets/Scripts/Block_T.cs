
using UnityEngine;

namespace AVDTetris
{
    class Block_T : BaseBlock
    {
        readonly int[,] prototype = new int[,] {
            { 1, 1, 1 },
            { 0, 1, 0 },
            { 0, 0, 0 }};

        public Block_T(GameObject prefab) : base(E_BlockType.T, prefab) { }

        protected override int[,] GetProtoMatrix() => prototype;

    }
}
