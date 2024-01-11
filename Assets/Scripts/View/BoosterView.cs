using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вью кнопок бустеров
    /// </summary>
    public class BoosterView : MonoBehaviour
    {
        [SerializeField] private UIDocument document;

        private Button button1;
        private Button button2;
        private Label counterText1;
        private Label counterText2;

        public Action<int> ButtonClicked;

        public void SetButtonActive(int index, bool isActive)
        {
            (index == 0 ? button1 : button2).SetEnabled(isActive);
        }
        
        public void SetButtonSprite(int index, Sprite sprite)
        {
            (index == 0 ? button1 : button2).style.backgroundImage = new StyleBackground(sprite);
        }
        
        public void SetCounterText(int index, float value, bool isVisible, string format = "")
        {
            var text = index == 0 ? counterText1 : counterText2;
            text.text = value.ToString(format);
            text.style.visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }
        
        public async UniTaskVoid SetCooldownTextAsync(int index, float cooldown, CancellationToken token)
        {
            try
            {
                var counter = cooldown;

                while (counter > 0)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
                    SetCounterText(index, counter, true, "0.0");
                    counter -= Time.deltaTime;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                SetCounterText(index, 0f, false);
            }
        }

        public void SetBoosterActivated(int index, bool isActive)
        {
            if (isActive)
            {
                (index == 0 ? button1 : button2).AddToClassList("button-booster-activated");
            }
            else
            {
                (index == 0 ? button1 : button2).RemoveFromClassList("button-booster-activated");
            }
        }

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            button1 = root.Q<Button>("button_booster_1");
            button2 = root.Q<Button>("button_booster_2");
            
            counterText1 = root.Q<Label>("text_booster_counter_1");
            counterText2 = root.Q<Label>("text_booster_counter_2");
            
            button1.clicked += OnButton1Click;
            button2.clicked += OnButton2Click;
        }

        private void OnDisable()
        {
            button1.clicked -= OnButton1Click;
            button2.clicked -= OnButton2Click;
        }

        private void OnButton1Click()
        {
            ButtonClicked?.Invoke(0);
        }

        private void OnButton2Click()
        {
            ButtonClicked?.Invoke(1);
        }
    }
}