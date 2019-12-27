using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dragablz;
using MyPortal.Desktop.Base.Commands;
using MyPortal.Desktop.Base.Interfaces;
using MyPortal.Desktop.Base.ViewModels;
using MyPortal.Desktop.ViewModels.Home;
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

        public AppViewModel()
        {
            OpenItems = new ObservableCollection<IAppWindow> {new StudentViewModel()};
            OpenItems.CollectionChanged += OpenItems_CollectionChanged;
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

        public ItemActionCallback ClosingTabItemHandler => ClosingTabItemHandlerImpl;

        private void ClosingTabItemHandlerImpl(ItemActionCallbackArgs<TabablzControl> args)
        {
            var viewModel = (IAppWindow) args.DragablzItem.DataContext;

            if (!viewModel.BeforeTabClosed())
            {
                args.Cancel();
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

        private void OpenItems_CollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems.OfType<IAppWindow>())
                {
                    oldItem.OnTabClosed();
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems.OfType<IAppWindow>())
                {
                    newItem.OnTabOpened();
                }
            }
        }

        internal void Remove(IAppWindow toRemove)
        {
            if (SelectedItem == toRemove)
            {
                SelectedItem = OpenItems.FirstOrDefault(i => i != toRemove);
            }
            if (OpenItems.Contains(toRemove))
                OpenItems.Remove(toRemove);
        }

        private void OpenStudents()
        {
            var vm = new StudentViewModel();
            AddAndSelect(vm);
        }
    }
}
