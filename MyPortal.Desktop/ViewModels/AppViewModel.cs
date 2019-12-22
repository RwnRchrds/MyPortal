using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyPortal.Desktop.Base.Commands;
using MyPortal.Desktop.Base.Interfaces;
using MyPortal.Desktop.Base.ViewModels;
using MyPortal.Desktop.ViewModels.People.Students;

namespace MyPortal.Desktop.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private bool _updating;
        private IAppWindow _selectedItem;
        private BaseCommand _openStudentsCommand;

        public ObservableCollection<IAppWindow> OpenItems { get; set; }
        public bool IsReady
        {
            get { return !Updating; }
        }
        public bool Updating
        {
            get { return _updating; }
            set
            {
                if (_updating == value)
                    return;
                _updating = value;
                OnPropertyChanged(() => Updating);
                OnPropertyChanged(() => IsReady);
            }
        }

        public IAppWindow SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem?.OnTabUnselected();
                _selectedItem = value;
                OnPropertyChanged(() => SelectedItem);
                _selectedItem?.OnTabSelected();
            }
        }

        public BaseCommand OpenStudentsCommand
        {
            get => _openStudentsCommand ?? (_openStudentsCommand = new BaseCommand(OpenStudents));
        }

        internal void AddAndSelect(IAppWindow windowToAdd)
        {
            if (Updating)
            {
                return;
            }

            if (!OpenItems.Contains(windowToAdd))
            {
                OpenItems.Add(windowToAdd);
            }

            SelectedItem = windowToAdd;
        }

        private void OpenStudents()
        {
            var vm = new StudentViewModel();
            AddAndSelect(vm);
        }
    }
}
