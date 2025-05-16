using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace IdeaManager.UI.ViewModels
{
    public partial class IdeaListViewModel : ObservableObject
    {
        private readonly IIdeaService _ideaService;

        [ObservableProperty]
        private ObservableCollection<Idea> ideas = new();

        public IAsyncRelayCommand<Idea> ApproveCommand { get; }
        public IAsyncRelayCommand<Idea> RejectCommand { get; }

        public IdeaListViewModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;

            ApproveCommand = new AsyncRelayCommand<Idea>(async idea =>
            {
                if (idea != null)
                {
                    await _ideaService.ApproveIdeaAsync(idea);
                    await LoadIdeasAsync();
                }
            });

            RejectCommand = new AsyncRelayCommand<Idea>(async idea =>
            {
                if (idea != null)
                {
                    await _ideaService.RejectIdeaAsync(idea);
                    await LoadIdeasAsync();
                }
            });

        }

        public async Task LoadIdeasAsync()
        {
            var ideasFromDb = await _ideaService.GetAllAsync();
            Ideas = new ObservableCollection<Idea>(ideasFromDb);
        }
    }
}
