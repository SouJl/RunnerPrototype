using Runner.Interfaces;
using System;

namespace Runner.Tool
{
    internal class SubscriptionProperty<T> : ISubscriptionProperty<T>
    {
        private T _value;
        private Action<T> _onChangeeValue;

        public T Value 
        {
            get => _value;
            set 
            {
                _value = value;
                _onChangeeValue?.Invoke(_value);
            }
        }

        public void SubscriptionOnChange(Action<T> subscriptionAction)
        {
            _onChangeeValue += subscriptionAction;
        }

        public void UnSubscriptionOnChange(Action<T> unsubscriptionAction)
        {
            _onChangeeValue -= unsubscriptionAction;
        }
    }
}
