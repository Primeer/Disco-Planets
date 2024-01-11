using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вью панели с номером текущего уровня
    /// </summary>
    public class LevelView : MonoBehaviour
    {
        private const string LevelLabelName = "text_level";
        
        [SerializeField] private UIDocument document;
        
        private Label levelLabel;


        public void SetLevelText(int level)
        {
            levelLabel.text = level.ToString();
        }

        private void OnEnable()
        {
            var root = document.rootVisualElement;
            levelLabel = root.Q<Label>(LevelLabelName);
        }
    }
}
