
using UnityEngine;

namespace AVDTetris
{
    class Block_S : BaseBlock
    {
        readonly int[,] prototype = new int[,] {
            { 0, 1, 1 },
            { 1, 1, 0 },
            { 0, 0, 0 }};

        public Block_S(GameObject prefab) : base(E_BlockType.S, prefab) { }

        protected override int[,] GetProtoMatrix() => prototype;

    }
}