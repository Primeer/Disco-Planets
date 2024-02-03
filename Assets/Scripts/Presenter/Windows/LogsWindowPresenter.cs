using Repository;
using UnityEngine.UIElements;
using VContainer;

namespace Presenter.Windows
{
    public class LogsWindowPresenter : WindowPresenter
    {
        [Inject] private readonly DebugLogsRepository logsRepository;

        protected override void OnShow()
        {
            var listView = WindowElement.Q<ListView>("list");
            listView.itemsSource = logsRepository.Logs;
            listView.makeItem = () => new Label();
            listView.bindItem = (label, i) => ((Label)label).text = logsRepository.Logs[i];
        }
    }
}