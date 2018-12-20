using System;

namespace Spotlight.Libs
{
    public class RelayCommand : CommandBase
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            if (canExecute == null)
                canExecute = (o) => true;

            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        protected override void OnExecute(object parameter)
        {
            execute(parameter);
        }
    }
}
