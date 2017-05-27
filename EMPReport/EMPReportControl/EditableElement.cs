//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    3dcef883-f0bd-4fac-aee4-0aa5cec59751
//        CLR Version:              4.0.30319.42000
//        Name:                     EditableElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                EditableElement
//
//        Created by Charley at 2017/4/25 15:36:19
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace NetInfo.EMP.Reports.Controls
{
    public class EditableElement : Control
    {
        private bool mCanEditable;

        static EditableElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableElement),
                new FrameworkPropertyMetadata(typeof(EditableElement)));

            EditableElementEventEvent = EventManager.RegisterRoutedEvent("EditableElementEvent", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<EditableElementEventArgs>), typeof(EditableElement));
        }

        public EditableElement()
        {
            mCanEditable = false;
        }


        #region IsInEditMode

        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register("IsInEditMode", typeof(bool), typeof(EditableElement), new PropertyMetadata(default(bool)));

        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        #endregion


        #region IsReadOnly

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(EditableElement), new PropertyMetadata(default(bool)));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        #endregion


        #region Text

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EditableElement), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion


        #region Template

        private const string PART_TextBlock = "PART_TextBlock";
        private const string PART_EditBox = "PART_EditBox";

        private TextBlock mTextBlock;
        private TextBox mEditBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mTextBlock = GetTemplateChild(PART_TextBlock) as TextBlock;
            if (mTextBlock != null)
            {
                mTextBlock.MouseUp += TextBlock_MouseUp;
                mTextBlock.MouseLeave += TextBlock_MouseLeave;
            }

            mEditBox = GetTemplateChild(PART_EditBox) as TextBox;
            if (mEditBox != null)
            {
                mEditBox.LostFocus += EditBox_LostFocus;
                mEditBox.KeyDown += EditBox_KeyDown;
                mEditBox.TextChanged += EditBox_TextChanged;
            }
        }

        #endregion


        #region Others

        public TextBlock TextBlock
        {
            get { return mTextBlock; }
        }

        public TextBox TextBox
        {
            get { return mEditBox; }
        }

        #endregion


        #region EventHandlers

        void EditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                mCanEditable = false;
                IsInEditMode = false;

                EditableElementEventArgs args = new EditableElementEventArgs();
                args.Code = EditableElementEventArgs.EVT_EDIT_END;
                args.Element = this;
                args.Data = e;
                OnEditableElementEvent(this, args);
            }
        }

        void EditBox_LostFocus(object sender, RoutedEventArgs e)
        {
            mCanEditable = false;
            IsInEditMode = false;

            EditableElementEventArgs args = new EditableElementEventArgs();
            args.Code = EditableElementEventArgs.EVT_EDIT_END;
            args.Element = this;
            args.Data = e;
            OnEditableElementEvent(this, args);
        }

        void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            mCanEditable = false;
        }

        void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mCanEditable)
            {
                IsInEditMode = true;

                if (mEditBox != null)
                {
                    mEditBox.Focus();
                }
            }
            else
            {
                mCanEditable = true;
            }
        }

        void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EditableElementEventArgs args = new EditableElementEventArgs();
            args.Code = EditableElementEventArgs.EVT_TEXT_CHANGED;
            args.Element = this;
            args.Data = e;
            OnEditableElementEvent(this, args);
        }

        #endregion


        #region EditableElementEvent

        public static readonly RoutedEvent EditableElementEventEvent;

        public event RoutedPropertyChangedEventHandler<EditableElementEventArgs> EditableElementEvent
        {
            add { AddHandler(EditableElementEventEvent, value); }
            remove { RemoveHandler(EditableElementEventEvent, value); }
        }

        protected void OnEditableElementEvent(object sender, EditableElementEventArgs e)
        {
            var element = sender as EditableElement;
            if (element != null)
            {
                RoutedPropertyChangedEventArgs<EditableElementEventArgs> args =
                    new RoutedPropertyChangedEventArgs<EditableElementEventArgs>(default(EditableElementEventArgs), e);
                args.RoutedEvent = EditableElementEventEvent;
                element.RaiseEvent(args);
            }
        }

        #endregion


    }
}
