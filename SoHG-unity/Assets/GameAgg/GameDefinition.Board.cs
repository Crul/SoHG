using UnityEngine;

namespace Sohg.GameAgg
{
    public partial class GameDefinition
    {
        [Header("Board")]
        [SerializeField]
        private int boardColumns;
        [SerializeField]
        private int boardRows;
        [SerializeField]
        private Texture2D boardBackground;
        [SerializeField]
        private Texture2D boardMask;

        public int BoardColumns { get { return boardColumns; } }
        public int BoardRows { get { return boardRows; } }
        public Texture2D BoardBackground { get { return boardBackground; } }
        public Texture2D BoardMask { get { return boardMask; } }
    }
}
