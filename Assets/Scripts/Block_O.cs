
using UnityEngine;

namespace AVDTetris
{
    class Block_O : BaseBlock
    {
        readonly int[,] prototype = new int[,] { 
            { 1, 1 }, 
            { 1, 1 }};

        public Block_O(GameObject prefab) : base(E_BlockType.O, prefab) { }

        protected override int[,] GetProtoMatrix() => prototype;

    }
}
