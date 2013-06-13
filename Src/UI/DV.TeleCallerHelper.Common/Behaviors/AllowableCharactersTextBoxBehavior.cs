using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DV.TeleCallerHelper.Common.Behaviors
{
    public class AllowableCharactersTextBoxBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty RegularExpressionProperty =
            DependencyProperty.Register("RegularExpression", typeof(string), typeof(AllowableCharactersTextBoxBehavior),
            new FrameworkPropertyMetadata("*"));
        public string RegularExpression
        {
            get
            {
                return (string)base.GetValue(RegularExpressionProperty);
            }
            set
            {
                base.SetValue(RegularExpressionProperty, value);
            }
        }

        public static readonly DependencyProperty MaxLengthProperty =
        DependencyProperty.Register("MaxLength", typeof(int), typeof(AllowableCharactersTextBoxBehavior),
        new FrameworkPropertyMetadata(int.MinValue));

        public int MaxLength
        {
            get
            {
                return (int)base.GetValue(MaxLengthProperty);
            }
            set
            {
                base.SetValue(MaxLengthProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("Command", typeof(ICommand), typeof(AllowableCharactersTextBoxBehavior), null);

        public ICommand Command
        {
            get
            {
                return (ICommand)base.GetValue(CommandProperty);
            }
            set
            {
                base.SetValue(CommandProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                if (Command != null && Command.CanExecute(null))
                {
                    Command.Execute(null);
                }

                string text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));
                bool exceedsMaxLength = false;
                if (MaxLength > 0)
                {
                    exceedsMaxLength = text.Length > MaxLength;
                }

                Regex regex = new Regex(RegularExpression);
                if (!regex.IsMatch(text) || exceedsMaxLength)
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool exceedsMaxLength = false;
            string text = this.AssociatedObject.Text.Insert(this.AssociatedObject.CaretIndex, e.Text);
            if (MaxLength > 0)
            {
                exceedsMaxLength = text.Length > MaxLength;
            }

            Regex regex = new Regex(RegularExpression);
            e.Handled = !regex.IsMatch(text) || exceedsMaxLength;

            if (!e.Handled)
            {
                if (Command != null && Command.CanExecute(null))
                {
                    Command.Execute(null);
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }
    }
}
