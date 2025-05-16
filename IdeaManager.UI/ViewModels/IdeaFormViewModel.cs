using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Interfaces;

namespace IdeaManager.UI.ViewModels
{
    public partial class IdeaFormViewModel : ObservableObject
    {
        private readonly IIdeaService _ideaService;

        public IdeaFormViewModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        [ObservableProperty] private string title = string.Empty;
        [ObservableProperty] private string description = string.Empty;
        [ObservableProperty] private string errorMessage = string.Empty;

        [RelayCommand]
        private async Task SubmitAsync()
        {
            try
            {
                var idea = new Idea { Title = Title, Description = Description };
                await _ideaService.SubmitIdeaAsync(idea);
                ErrorMessage = string.Empty;
                MessageBox.Show("Idée enregistrée !");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
