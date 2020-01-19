﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPFSokoban.ViewModels;
using WPFSokoban.ModelGenerator;
using MapHandlerWPF;
using System;
using System.ComponentModel;

namespace WPFSokoban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel ViewModel = new MainWindowViewModel();
        CellViewModelUtilities modelUtilities = new CellViewModelUtilities();


        public MainWindow()
        {
            InitializeComponent();
            InputHandler handler = InputHandler.Instance;

            this.ViewModel.CellsCollection = modelUtilities.MapGenerator(handler.MapState, handler.MapXSize, handler.MapYSize);

            //bind the generated map
            map.DataContext = this.ViewModel;

            handler.MapStateChanged += OnMapStateChanged;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            InputHandler handler = InputHandler.Instance;
            handler.HandleKeyPress(e.Key);
            base.OnKeyUp(e);
        }

        private void OnMapStateChanged(object sender, EventArgs e)
        {
            InputHandler handler = InputHandler.Instance;

            this.ViewModel.CellsCollection = modelUtilities.MapGenerator(handler.MapState, handler.MapXSize, handler.MapYSize);
        }

    }
}
