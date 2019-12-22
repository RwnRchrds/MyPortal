using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyPortal.Desktop.Base.Interfaces;

namespace MyPortal.Desktop.Base.ViewModels
{
    public abstract class BaseViewModel : IBaseVM
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            OnPropertyChanged(((MemberExpression)action.Body).Member.Name);
        }

        public virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected static Boolean IsInDesignMode()
        {
            var prop = DesignerProperties.IsInDesignModeProperty;
            return (bool)DependencyPropertyDescriptor
                              .FromProperty(prop, typeof(FrameworkElement))
                              .Metadata.DefaultValue;
        }

        public virtual Boolean HasChanges
        {
            get { return false; }
        }

        public static bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
    }
    public class Lookup : Lookup<LookupResult>
    {
        public virtual void SetInitialValue(Guid? id)
        {
            SetInitialValue(Items.FirstOrDefault(i => i.ID == id));
        }
    }

    public abstract class BaseViewModelWithItems<T> : BaseViewModel where T : IBaseVM
    {
        public BaseViewModelWithItems()
        {
            _items = new List<T>();
        }

        private readonly List<T> _items;
        public virtual void AddChild(T child, Boolean noitifyCollectionChanged = false)
        {
            _items.Add(child);
            if (noitifyCollectionChanged)
                OnPropertyChanged(() => Items);
        }

        public void Clear()
        {
            _items.Clear();
            OnPropertyChanged(() => Items);
        }
        protected virtual void Remove(T toRemove, Boolean notifyChanges)
        {
            _items.Remove(toRemove);
            if (notifyChanges)
                OnPropertyChanged(() => Items);
        }

        public T[] Items { get { return _items.ToArray(); } }
        protected virtual void HasChangesChanged()
        {
            OnPropertyChanged(() => HasChanges);
        }

        public override bool HasChanges
        {
            get
            {
                var iHasChanges = Items.OfType<IBaseVM>();
                var changes = iHasChanges.Where(i => i.HasChanges);
                return changes.Any(i => i.HasChanges);
            }
        }
    }

    public class Lookup<T> : BaseViewModelWithItems<T> where T : IBaseVM
    {
        public override bool HasChanges
        {
            get { return !EqualityComparer<T>.Default.Equals(_initialValue, _selectedItem); }
        }

        public String Prompt { get; set; }

        protected T _initialValue;

        internal void SetInitialValue(T value)
        {
            _initialValue = value;
            _selectedItem = value;
            OnPropertyChanged(() => SelectedItem);
        }


        public void AddChildRange(T[] toAdd, bool chainHasChanges = true, bool noitifyCollectionChanged = false)
        {
            foreach (var child in toAdd)
                AddChild(child, chainHasChanges);
            if (noitifyCollectionChanged)
                OnPropertyChanged(() => Items);
        }
        internal Boolean Required { get; set; }

        private T _selectedItem;

        public virtual T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (EqualityComparer<T>.Default.Equals(_selectedItem, value)) return;
                var oldValue = _selectedItem;
                _selectedItem = value;
                OnPropertyChanged(() => SelectedItem);
                OnPropertyChanged(() => HasChanges);
                OnValueChanged(oldValue);
            }
        }
        internal virtual void OnValueChanged(T oldValue)
        {
            if (ValueChanged != null)
                ValueChanged(this, new ValueChangedEventArgs(SelectedItem, oldValue));
        }
        private Boolean _enabled = true;

        public Boolean Enabled
        {
            get { return _enabled; }
            set
            {
                if (EqualityComparer<Boolean>.Default.Equals(_enabled, value)) return;
                _enabled = value;
                OnPropertyChanged(() => Enabled);
            }
        }
        private bool _isReadOnly;
        public Boolean IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                if (_isReadOnly == value) return;
                _isReadOnly = value;
                OnPropertyChanged(() => IsReadOnly);
            }
        }

        public event ValueChangedEvent ValueChanged;

        public delegate void ValueChangedEvent(Lookup<T> sender, ValueChangedEventArgs e);
        public class ValueChangedEventArgs : EventArgs
        {
            public ValueChangedEventArgs(T newItem, T oldItem)
            {
                _newItem = newItem;
                _oldItem = oldItem;
            }
            private readonly T _newItem;
            public T NewItem
            {
                get { return _newItem; }
            }

            private readonly T _oldItem;
            public T OldItem
            {
                get { return _oldItem; }
            }
        }
    }
}
