
using UnityEngine;

namespace AVDTetris
{
    class Block_L : BaseBlock
    {
        readonly int[,] prototype = new int[,] { 
            { 0, 1, 0 }, 
            { 0, 1, 0 }, 
            { 0, 1, 1 }};

        public Block_L(GameObject prefab) : base(E_BlockType.L, prefab) { }

        protected override int[,] GetProtoMatrix() => prototype;

    }
}
