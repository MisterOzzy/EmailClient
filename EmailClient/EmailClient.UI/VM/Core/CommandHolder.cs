using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Input;

namespace EmailClient.UI.VM.Core
{
    public class CommandHolder
    {
        private readonly Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public ICommand GetOrCreateCommand<T>(Expression<Func<T>> commandNameExpression, Action executeCommandAction)
        {
            return GetOrCreateCommand(commandNameExpression, executeCommandAction, () => true);
        }

        public ICommand GetOrCreateCommand<T>(Expression<Func<T>> commandNameExpression,
            Action<object> executeCommandAction)
        {
            var propertyName = SymbolHelpers.GetPropertyName(commandNameExpression);
            if (!_commands.ContainsKey(propertyName))
            {
                var relayCommand = new RelayCommand(executeCommandAction);
                _commands.Add(propertyName, relayCommand);
            }
            return _commands[propertyName];
        }

        public ICommand GetOrCreateCommand<T>(Expression<Func<T>> commandNameExpression, Action executeCommandAction,
            Func<bool> canExecutePredicate)
        {
            return GetOrCreateCommand(commandNameExpression, parameter => executeCommandAction(),
                parameter => canExecutePredicate());
        }

        public ICommand GetOrCreateCommand<T>(Expression<Func<T>> commandNameExpression,
            Action<object> executeCommandAction, Func<object, bool> canExecutePredicate)
        {
            var propertyName = SymbolHelpers.GetPropertyName(commandNameExpression);
            if (!_commands.ContainsKey(propertyName))
            {
                var relayCommand = new RelayCommand(executeCommandAction, canExecutePredicate);
                _commands.Add(propertyName, relayCommand);
            }
            return _commands[propertyName];
        }
    }
}