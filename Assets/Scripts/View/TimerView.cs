using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вьюха прогресс бара таймера, который отображается при превышении лимита шаров
    /// </summary>
    public class TimerView : MonoBehaviour
    {
        private const string ProgressbarName = "panel_progressbar_time";
        private const string FillerName = "panel_progressbar_time_filler";
        private const string TextName = "text_progressbar_time";
        
        [SerializeField] private UIDocument document;
        
        private VisualElement progressbar;
        private VisualElement filler;
        private Label text;


        public void SetTimer(bool isActive, float time = 0f, float maxTime = 0f)
        {
            progressbar.style.visibility = isActive ? Visibility.Visible : Visibility.Hidden;

            if (!isActive)
            {
                return;
            }
            
            text.text = time.ToString("F1");
            
            float progress = Mathf.Clamp01(time / maxTime);
            filler.style.width = new StyleLength(new Length(progress * 100, LengthUnit.Percent));
        }
        
        private void OnEnable()
        {
            var root = document.rootVisualElement;
            
            progressbar = root.Q<VisualElement>(ProgressbarName);
            filler = root.Q<VisualElement>(FillerName);
            text = root.Q<Label>(TextName);
            
            progressbar.style.visibility = Visibility.Hidden;
        }
    }
}