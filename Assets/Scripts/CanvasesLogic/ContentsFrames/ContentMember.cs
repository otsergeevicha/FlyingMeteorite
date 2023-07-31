using Plugins.MonoCache;
using TMPro;
using UnityEngine;

namespace CanvasesLogic.ContentsFrames
{
    public class ContentMember : MonoCache
    {
        [SerializeField] private TMP_Text _position;
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private TMP_Text _score;

        public void InitData(int position, string nickName, int score)
        {
            _position.text = position.ToString();
            _nickName.text = nickName;
            _score.text = score.ToString();
        }
    }
}