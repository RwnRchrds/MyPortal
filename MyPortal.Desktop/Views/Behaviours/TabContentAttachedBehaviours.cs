using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Syncfusion.Windows.Tools.Controls;

namespace MyPortal.Desktop.Views.Behaviours
{
    public static class TabContentAttachedBehaviours
    {
        private static Ribbon _ribbon;

        public static ConcurrentDictionary<Type, ContextTabGroup> ContextualTabGroups { get; } =
            new ConcurrentDictionary<Type, ContextTabGroup>();

        public static Ribbon GetRibbon(FrameworkElement frameworkElement)
        {
            return (Ribbon) frameworkElement.GetValue(RibbonProperty);
        }

        public static void SetRibbon(FrameworkElement frameworkElement, Ribbon value)
        {
            frameworkElement.SetValue(RibbonProperty, value);
        }

        public static readonly DependencyProperty RibbonProperty = DependencyProperty.RegisterAttached("Ribbon",
            typeof(Ribbon), typeof(TabContentAttachedBehaviours), new UIPropertyMetadata(null, RibbonChanged));

        public static void PopulateContextualTabGroups(Type type, Ribbon ribbon)
        {
            _ribbon = ribbon;

            if (ContextualTabGroups.ContainsKey(type))
            {
                ContextTabGroup tabGroup = ContextualTabGroups[type];

                foreach (ContextTabGroup contextTabGroup in ribbon.ContextTabGroups)
                {
                    foreach (var ribbonTab in contextTabGroup.RibbonTabs)
                    {
                        var binding = BindingOperations.GetBinding((RibbonTab) ribbonTab, FrameworkElement.DataContextProperty);
                        if (binding != null)
                        {
                            BindingOperations.ClearBinding((RibbonTab) ribbonTab, FrameworkElement.DataContextProperty);
                        }
                    }
                }

                ribbon.ContextTabGroups.Clear();

                if (tabGroup != null)
                {
                    ribbon.ContextTabGroups.Add(tabGroup);
                    ((RibbonTab) tabGroup.RibbonTabs.First()).IsChecked = true;

                    foreach (var tab in tabGroup.RibbonTabs)
                    {
                        Binding binding = new Binding("SelectedItem") { Source = WindowInstance.Value };
                        BindingOperations.SetBinding((RibbonTab) tab, FrameworkElement.DataContextProperty, binding);
                    }
                }
            }
        }


        private static void RibbonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement frameworkElement = d as FrameworkElement;

            var type = frameworkElement.DataContext.GetType();

            if (!ContextualTabGroups.ContainsKey(type))
            {
                ContextualTabGroups.TryAdd(type, frameworkElement.TryFindResource("ContextMenu") as ContextTabGroup);

                if (_ribbon != null)
                {
                    PopulateContextualTabGroups(type, _ribbon);
                }
            }
        }
    }
}
