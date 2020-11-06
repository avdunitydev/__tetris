
using UnityEngine;

namespace AVDTetris
{
    class Block_I : BaseBlock
    {
        readonly int[,] prototype = new int[,] {
            { 0, 1, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 1, 0, 0 }};

        public Block_I(GameObject prefab) : base(E_BlockType.I, prefab) { }

        protected override int[,] GetProtoMatrix() => prototype;

    }
}
