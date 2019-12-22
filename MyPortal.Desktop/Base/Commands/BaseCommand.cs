using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MyPortal.Desktop.Base.ViewModels;

namespace MyPortal.Desktop.Base.Commands
{
    public class BaseCommand : BaseViewModel, ICommand
    {
        public BaseCommand(Action<object> execute, Func<Boolean> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            ActionsWithParams = new List<Action<object>> { execute };
            Actions = new List<Action>();
            CanExecute = canExecute ?? (() => true);
        }

        public BaseCommand(Action execute, Func<Boolean> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            Actions = new List<Action> { execute };
            ActionsWithParams = new List<Action<object>>();
            CanExecute = canExecute ?? (() => true);
        }

        private static bool CanDoNothing()
        {
            return false;
        }

        private static void DoNothing()
        {
            //thats right, nothing here
        }



        public static BaseCommand DoNothingCommand = new BaseCommand(DoNothing, CanDoNothing);
        private List<Action<object>> ActionsWithParams { get; set; }
        private List<Action> Actions { get; set; }
        private Func<Boolean> CanExecute { get; set; }

        private String _prompt;

        public Boolean Enabled { get { return CanExecute(); } }

        public void RegisterAction(Action execute)
        {
            Actions.Add(execute);
        }
        public void RegisterAction(Action<object> execute)
        {
            ActionsWithParams.Add(execute);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter = null)
        {
            Actions.ForEach(a => a());
            ActionsWithParams.ForEach(a => a(parameter));
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, new EventArgs());
                OnPropertyChanged(() => Enabled);
            }));
        }

        public String Prompt
        {
            get { return _prompt; }
            set
            {
                if (_prompt == value) return;
                _prompt = value;
                OnPropertyChanged(() => Prompt);
            }
        }
    }
    public class BaseCommand<T> : BaseViewModel, ICommand
    {
        private readonly Action<T> _command;
        private readonly Func<bool> _canExecute;

        public BaseCommand(Action<T> command, Func<bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            _canExecute = canExecute;
            _command = command;
        }

        private String _label;
        public String Label
        {
            get { return _label; }
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged(() => Label);
            }
        }

        //public string Label { get; internal set; }
        public string Image { get; internal set; }

        public void Execute(object parameter)
        {
            _command((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute();
        }

        public Boolean Enabled { get { return CanExecute(null); } }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                Dispatcher.CurrentDispatcher.BeginInvoke(CanExecuteChanged, new object[] { this, null });
            OnPropertyChanged(() => Enabled);
        }
        public event EventHandler CanExecuteChanged;

        public static BaseCommand DoNothingCommand = new BaseCommand(DoNothing, CanDoNothing);

        private static bool CanDoNothing()
        {
            return false;
        }

        private static void DoNothing()
        {
            //thats right, nothing here
        }
    }
}
