using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Models.Base
{

    public class clsModelBase : clsNotifyDataErrorInfoBase, INotifyPropertyChanged, IValidatableTrackingObject
    {

        #region Members
        public Dictionary<string, object> _originalValues;
        public List<IValidatableTrackingObject> _trackingObjects;
        public List<DelegateCommand> Commands = new List<DelegateCommand>();

        #endregion


        #region Properties

        //public T Model { get;  set; }
        public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(t => t.IsChanged);

        public bool IsValid => !HasErrors && _trackingObjects.All(t => t.IsValid);




        #endregion

        #region Constructors
        public clsModelBase()
        {
            _originalValues = new Dictionary<string, object>();
            _trackingObjects = new List<IValidatableTrackingObject>();
            //Validate();

        }

        #endregion


        //protected TValue GetValue<TValue>(object model,[CallerMemberName] string propertyName = null)
        //{
        //    var propertyInfo = model.GetType().GetProperty(propertyName);



        //    return (TValue) propertyInfo.GetValue(model);
        //}

        protected void SetValue<TValue>(ref TValue currentValue, TValue newValue, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = GetType().GetProperty(propertyName);
            //var currentValue = propertyInfo.GetValue();

            if (!Equals(currentValue, newValue))
            {
                UpdateOriginalValue(currentValue, newValue, propertyName);
                //propertyInfo.SetValue(this, newValue);

                currentValue = newValue;
                Validate();
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName + "_IsChanged");

            }
        }




        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            if (_originalValues.ContainsKey(propertyName))
            {
                return (TValue)_originalValues[propertyName];
            }
            else
            {
                var propertyInfo = this.GetType().GetProperty(propertyName);
                return (TValue)propertyInfo.GetValue(this);
            }



        }

        protected bool GetIsChanged(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName);
        }
        private void UpdateOriginalValue(object currentValue, object newValue, string propertyName)
        {

            if (!_originalValues.ContainsKey(propertyName))
            {
                _originalValues.Add(propertyName, currentValue);
                OnPropertyChanged(nameof(IsChanged));
            }
            else
            {
                if (Equals(_originalValues[propertyName], newValue))
                {
                    _originalValues.Remove(propertyName);
                    OnPropertyChanged(nameof(IsChanged));
                }
            }
        }



        public void Validate()
        {
            ClearErrors();

            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);

            if (results.Any())
            {
                var propertyNames = results.SelectMany(r => r.MemberNames).Distinct().ToList();

                foreach (var propertyName in propertyNames)
                {
                    Errors[propertyName] = results
                      .Where(r => r.MemberNames.Contains(propertyName))
                      .Select(r => r.ErrorMessage)
                      .Distinct()
                      .ToList();
                    OnErrorsChanged(propertyName);
                }
            }
            OnPropertyChanged(nameof(IsValid));
        }

        protected void RegisterTrackingObject(IValidatableTrackingObject trackingObject)
        {
            if (!_trackingObjects.Contains(trackingObject))
            {
                _trackingObjects.Add(trackingObject);
                trackingObject.PropertyChanged += TrackingObjectPropertyChanged;
            }
        }

        private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsChanged))
            {
                OnPropertyChanged(nameof(IsChanged));
            }
            else if (e.PropertyName == nameof(IsValid))
            {
                OnPropertyChanged(nameof(IsValid));
            }
            Validate();
        }


        //protected void ClearErrors()
        //{
        //    foreach (var propertyName in Errors.Keys.ToList())
        //    {
        //        Errors.Remove(propertyName);
        //        OnErrorsChanged(propertyName);
        //    }
        //}

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RejectChanges()
        {
            //foreach (var originalValueEntry in _originalValues)
            //{
            //    typeof(T).GetProperty(originalValueEntry.Key).SetValue(Model, originalValueEntry.Value);
            //}
            //_originalValues.Clear();
            //foreach (var trackingObject in _trackingObjects)
            //{
            //    trackingObject.RejectChanges();
            //}
            //Validate();
            //OnPropertyChanged("");
        }

        public void AcceptChanges()
        {
            _originalValues.Clear();
            foreach (var trackingObjects in _trackingObjects)
            {
                trackingObjects?.AcceptChanges();
            }
            OnPropertyChanged("");
        }


        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        #endregion



    }
}
