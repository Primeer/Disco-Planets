using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вьюха прогресс бара количества шаров
    /// </summary>
    public class BallsLimitView : MonoBehaviour
    {
        private const string FillerName = "panel_progressbar_count_filler";
        private const string TextName = "text_progressbar_count";
        
        [Tooltip("Начальный цвет прогрессбара")]
        [SerializeField] private Color startColor;
        
        [Tooltip("Конечный цвет прогрессбара")]
        [SerializeField] private Color endColor;
        
        [SerializeField] private UIDocument document;
        
        private VisualElement filler;
        private Label text;


        public void SetProgress(int current, int limit)
        {
            text.text = $"{current}/{limit}";
            
            float progress = Mathf.Clamp01((float) current / limit);
            filler.style.width = new StyleLength(new Length(progress * 100, LengthUnit.Percent));
            filler.style.backgroundColor = new StyleColor(Color.Lerp(startColor, endColor, progress));
        }
        
        private void OnEnable()
        {
            var root = document.rootVisualElement;
            
            filler = root.Q<VisualElement>(FillerName);
            text = root.Q<Label>(TextName);
        }
    }
}
