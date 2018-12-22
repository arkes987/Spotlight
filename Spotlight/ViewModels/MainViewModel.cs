using Spotlight.Libs;
using Spotlight.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace Spotlight.ViewModels
{
    public class MainViewModel : NotifyObject
    {
        public MainViewModel()
        {
            _directory = new Models.Directory();
        }

        private readonly Models.Directory _directory;

        private bool _showInTaskbar = false;
        public bool ShowInTaskbar
        {
            get => _showInTaskbar;
            set
            {
                if (_showInTaskbar == value) return;
                _showInTaskbar = value;
                OnPropertyChanged(() => ShowInTaskbar);
            }
        }


        private List<Item> _files = new List<Item>();

        private int _filesCount = 0;
        public int FilesCount
        {
            get => Files.Count;
            set
            {
                if (_filesCount == value) return;
                _filesCount = value;
                OnPropertyChanged(() => FilesCount);
            }
        }
        public List<Item> Files
        {
            get => _files;
            set
            {
                if (_files == value) return;
                _files = value;
                FilesCount = _files.Count;
                OnPropertyChanged(() => Files);
            }
        }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                if (_searchInput == value) return;
                _searchInput = value;
                OnPropertyChanged(() => SearchInput);
            }
        }

        private Item _selectedFile;
        public Item SelectedFile
        {
            get => _selectedFile;
            set
            {
                if (_selectedFile == value) return;
                _selectedFile = value;
                OnPropertyChanged(() => SelectedFile);
            }
        }

        private int _selectedFileIndex;
        public int SelectedFileIndex
        {
            get => _selectedFileIndex;
            set
            {
                if (_selectedFileIndex == value) return;
                _selectedFileIndex = value;
                OnPropertyChanged(() => SelectedFileIndex);
            }
        }

        #region Search

        RelayCommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new RelayCommand(SearchCommandExecute, SearchCommandCanExecute));

        void SearchCommandExecute(object param)
        {
            Files = _directory.Retrack(SearchInput);

            if (Files.Count > 0)
                SelectedFile = Files[0];
        }

        bool SearchCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Run

        RelayCommand _runCommand;
        public ICommand RunCommand => _runCommand ?? (_runCommand = new RelayCommand(RunCommandExecute, RunCommandCanExecute));

        void RunCommandExecute(object param)
        {
            if (File.Exists(SelectedFile.Path))
            {
                Process.Start(SelectedFile.Path);
                SearchInput = string.Empty;
            }

        }

        bool RunCommandCanExecute(object param)
        {
            return SelectedFile != null;
        }

        #endregion

        #region GoUp

        RelayCommand _goUpCommand;
        public ICommand GoUpCommand => _goUpCommand ?? (_goUpCommand = new RelayCommand(GoUpCommandExecute, GoUpCommandCanExecute));

        void GoUpCommandExecute(object param)
        {
            if (SelectedFileIndex == 0) return;

            var prevIndex = --SelectedFileIndex;
            if (Files.Count > prevIndex)
                SelectedFile = Files[prevIndex];
        }

        bool GoUpCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region GoDown

        RelayCommand _goDownCommand;
        public ICommand GoDownCommand => _goDownCommand ?? (_goDownCommand = new RelayCommand(GoDownCommandExecute, GoDownCommandCanExecute));

        void GoDownCommandExecute(object param)
        {
            if (SelectedFileIndex == Files.Count - 1) return;

            var nextIndex = ++SelectedFileIndex;
            if (Files.Count > nextIndex)
                SelectedFile = Files[nextIndex];
        }

        bool GoDownCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

    }
}
