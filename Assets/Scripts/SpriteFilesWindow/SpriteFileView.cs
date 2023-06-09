using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Gamesture.Assets.Scripts.ScrollListWindow;

namespace Gamesture.Assets.Scripts
{
    public class SpriteFileView : MonoBehaviour, IPoolable<SpriteFileModel, IMemoryPool>, IScrollListElementView
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TMPro.TMP_Text _nameLabel;
        [SerializeField]
        private TMPro.TMP_Text _timeSpanLabel;

        private IMemoryPool _pool;

        [field: SerializeField]
        public RectTransform RectTransform { get; private set; }

        void IPoolable<SpriteFileModel, IMemoryPool>.OnSpawned(SpriteFileModel spriteFileData, IMemoryPool pool)
        {
            _pool = pool;
            _image.sprite = spriteFileData.Sprite;
            _nameLabel.SetText("File name: " + spriteFileData.FileName);
            _timeSpanLabel.SetText("Time span since creation:\n"
                + spriteFileData.TimeSinceCreation.ToString("dd' days 'hh' hours 'mm' minutes 'ss' seconds'"));
        }

        void IPoolable<SpriteFileModel, IMemoryPool>.OnDespawned()
        {
            _pool = null;
        }

        void IScrollListElementView.Despawn()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<SpriteFileModel, SpriteFileView> { }
    }
}
