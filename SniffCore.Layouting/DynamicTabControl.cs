using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Enhances the TabControl with buttons for add new tab item and close buttons of existing tab items.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <controls:DynamicTabControl ShowAddButton="True"
    ///                             TabItemAddingCommand="{Binding AddItemCommand}"
    ///                             TabItemClosingCommand="{Binding RemoveItemCommand}" />
    /// ]]>
    /// </code>
    /// </example>
    public class DynamicTabControl : TabControl
    {
        /// <summary>
        ///     Identifies the <see cref="ShowCloseButtons" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowCloseButtonsProperty =
            DependencyProperty.Register("ShowCloseButtons", typeof(bool), typeof(DynamicTabControl), new UIPropertyMetadata(true));

        /// <summary>
        ///     Identifies the <see cref="ShowAddButton" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowAddButtonProperty =
            DependencyProperty.Register("ShowAddButton", typeof(bool), typeof(DynamicTabControl), new UIPropertyMetadata(true));

        /// <summary>
        ///     Identifies the <see cref="TabItemClosingCommand" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TabItemClosingCommandProperty =
            DependencyProperty.Register("TabItemClosingCommand", typeof(ICommand), typeof(DynamicTabControl), new UIPropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="TabItemAddingCommandParameter" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TabItemAddingCommandParameterProperty =
            DependencyProperty.Register("TabItemAddingCommandParameter", typeof(object), typeof(DynamicTabControl), new UIPropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="TabItemAddingCommand" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TabItemAddingCommandProperty =
            DependencyProperty.Register("TabItemAddingCommand", typeof(ICommand), typeof(DynamicTabControl), new UIPropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="AddButtonMargin" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AddButtonMarginProperty =
            DependencyProperty.Register("AddButtonMargin", typeof(Thickness), typeof(DynamicTabControl), new UIPropertyMetadata(new Thickness(0)));

        /// <summary>
        ///     Identifies the <see cref="AddButtonWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AddButtonWidthProperty =
            DependencyProperty.Register("AddButtonWidth", typeof(double), typeof(DynamicTabControl), new UIPropertyMetadata(14.0));

        /// <summary>
        ///     Identifies the <see cref="AddButtonHeight" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AddButtonHeightProperty =
            DependencyProperty.Register("AddButtonHeight", typeof(double), typeof(DynamicTabControl), new UIPropertyMetadata(14.0));

        /// <summary>
        ///     Identifies the <see cref="AddButtonPosition" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AddButtonPositionProperty =
            DependencyProperty.Register("AddButtonPosition", typeof(Dock), typeof(DynamicTabControl), new UIPropertyMetadata(Dock.Right));

        static DynamicTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicTabControl), new FrameworkPropertyMetadata(typeof(DynamicTabControl)));
        }

        /// <summary>
        ///     Gets or sets a value which indicates if the close buttons are shown on the tab items header.
        /// </summary>
        [DefaultValue(true)]
        public bool ShowCloseButtons
        {
            get => (bool) GetValue(ShowCloseButtonsProperty);
            set => SetValue(ShowCloseButtonsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value which indicates if the add new tab item button is shown.
        /// </summary>
        [DefaultValue(true)]
        public bool ShowAddButton
        {
            get => (bool) GetValue(ShowAddButtonProperty);
            set => SetValue(ShowAddButtonProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command which gets called when the close on the tab item header is clicked. The tab DataContext is
        ///     forwarded as the command parameter.
        /// </summary>
        [DefaultValue(null)]
        public ICommand TabItemClosingCommand
        {
            get => (ICommand) GetValue(TabItemClosingCommandProperty);
            set => SetValue(TabItemClosingCommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the parameter which is passed with the <see cref="TabItemAddingCommand" /> command.
        /// </summary>
        [DefaultValue(null)]
        public object TabItemAddingCommandParameter
        {
            get => GetValue(TabItemAddingCommandParameterProperty);
            set => SetValue(TabItemAddingCommandParameterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command which gets called when the add new tab item button is pressed.
        /// </summary>
        [DefaultValue(null)]
        public ICommand TabItemAddingCommand
        {
            get => (ICommand) GetValue(TabItemAddingCommandProperty);
            set => SetValue(TabItemAddingCommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the margin of the add new tab item button.
        /// </summary>
        public Thickness AddButtonMargin
        {
            get => (Thickness) GetValue(AddButtonMarginProperty);
            set => SetValue(AddButtonMarginProperty, value);
        }

        /// <summary>
        ///     Gets or sets the width of the add new tab item button.
        /// </summary>
        [DefaultValue(14.0)]
        public double AddButtonWidth
        {
            get => (double) GetValue(AddButtonWidthProperty);
            set => SetValue(AddButtonWidthProperty, value);
        }

        /// <summary>
        ///     Gets or sets the height of the add new tab item button.
        /// </summary>
        [DefaultValue(14.0)]
        public double AddButtonHeight
        {
            get => (double) GetValue(AddButtonHeightProperty);
            set => SetValue(AddButtonHeightProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value which indicates where the add new tab item button has to be placed in the header.
        /// </summary>
        [DefaultValue(Dock.Right)]
        public Dock AddButtonPosition
        {
            get => (Dock) GetValue(AddButtonPositionProperty);
            set => SetValue(AddButtonPositionProperty, value);
        }

        /// <summary>
        ///     Generates a new child item container to hold in the <see cref="DynamicTabControl" />.
        /// </summary>
        /// <returns>The generated child item container</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DynamicTabItem();
        }

        /// <summary>
        ///     Checks if the item is already the correct item container. If not the <see cref="GetContainerForItemOverride" />
        ///     will be used to generate the right container.
        /// </summary>
        /// <param name="item">The item to shown in the <see cref="DynamicTabControl" />.</param>
        /// <returns>True if the item is the correct item container already.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DynamicTabItem;
        }
    }
}