using System;
using UnityEngine.UIElements;

namespace View.Windows
{
    /// <summary>
    /// Вьюха окна
    /// </summary>
    public class WindowView
    {
        private const string ButtonName = "button_continue";

        protected VisualElement Root;

        public Action ContinueButtonClicked;

        public virtual void Initialize(VisualElement root)
        {
            Root = root;
            
            var button = Root.Q<Button>(ButtonName);
            button?.RegisterCallback<ClickEvent>(OnButtonClicked);
        }

        public bool Visible
        {
            get => Root.style.display == DisplayStyle.Flex;
            set => Root.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void OnButtonClicked(ClickEvent _)
        {
            ContinueButtonClicked?.Invoke();
        }
    }
}