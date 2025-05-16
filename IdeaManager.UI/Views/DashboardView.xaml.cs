﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace IdeaManager.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Page
    {
        private readonly IServiceProvider _serviceProvider;

        public DashboardView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void OnSubmitIdeaClick(object sender, RoutedEventArgs e)
        {
            var ideaFormView = _serviceProvider.GetRequiredService<IdeaFormView>();
            NavigationService?.Navigate(ideaFormView);
        }

        private void OnViewIdeasClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var ideaListView = _serviceProvider.GetRequiredService<IdeaListView>();
                NavigationService?.Navigate(ideaListView);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation failed: " + ex.Message);
            }
        }

        private void OnViewProjectsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var projectListView = _serviceProvider.GetRequiredService<ProjectListView>();
                NavigationService?.Navigate(projectListView);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation vers les projets échouée : " + ex.Message);
            }
        }


    }
}
