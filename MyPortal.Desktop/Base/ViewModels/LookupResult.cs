using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Desktop.Base.ViewModels
{
    public class LookupResult : BaseViewModel, ILookupResultViewModel
    {
        public Guid? ID { get; set; }

        protected String _code;

        public virtual String Code
        {
            get { return _code; }
            set
            {
                if (EqualityComparer<String>.Default.Equals(_code, value)) return;
                _code = value;
                OnPropertyChanged(() => Code);
                OnPropertyChanged(() => Text);
            }
        }


        protected String _description;

        public virtual String Description
        {
            get { return _description; }
            set
            {
                if (EqualityComparer<String>.Default.Equals(_description, value)) return;
                _description = value;
                OnPropertyChanged(() => Description);
                OnPropertyChanged(() => Text);
            }
        }

        public virtual String Text
        {
            get
            {
                //Some lookup don't use code and description
                if (String.IsNullOrWhiteSpace(Code))
                    return Description;
                return $"{Code} - {Description}";
            }
        }

        protected Boolean _deleted;

        public virtual Boolean Deleted
        {
            get { return _deleted; }
            set
            {
                if (EqualityComparer<Boolean>.Default.Equals(_deleted, value)) return;
                _deleted = value;
                OnPropertyChanged(() => Deleted);
            }
        }
    }

    public interface ILookupResultViewModel : INotifyPropertyChanged
    {
        Guid? ID { get; }
        String Code { get; }
        String Description { get; }
        String Text { get; }
        Boolean Deleted { get; }
    }

    public interface ILookupResultMultipleViewModel : ILookupResultViewModel
    {
        Boolean IsSelected { get; set; }
    }
}
