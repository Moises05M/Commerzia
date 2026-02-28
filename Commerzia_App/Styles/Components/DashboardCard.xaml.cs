using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FontAwesome.Sharp;

namespace Commerzia_App.Styles.Components
{
    public partial class DashboardCard : UserControl
    {
        public DashboardCard()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DashboardCard));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(DashboardCard));

        public string Trend
        {
            get { return (string)GetValue(TrendProperty); }
            set { SetValue(TrendProperty, value); }
        }
        public static readonly DependencyProperty TrendProperty = DependencyProperty.Register("Trend", typeof(string), typeof(DashboardCard));

        public Brush TrendColor
        {
            get { return (Brush)GetValue(TrendColorProperty); }
            set { SetValue(TrendColorProperty, value); }
        }
        public static readonly DependencyProperty TrendColorProperty = DependencyProperty.Register("TrendColor", typeof(Brush), typeof(DashboardCard));

        // Icono
        public IconChar Icon
        {
            get { return (IconChar)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(IconChar), typeof(DashboardCard));

        // Color del Ícono
        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }
        public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.Register("IconForeground", typeof(Brush), typeof(DashboardCard));

        // Fondo del Ícono
        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }
        public static readonly DependencyProperty IconBackgroundProperty = DependencyProperty.Register("IconBackground", typeof(Brush), typeof(DashboardCard));

        // Ícono de Tendencia (Flecha arriba o abajo)
        public IconChar TrendIcon
        {
            get { return (IconChar)GetValue(TrendIconProperty); }
            set { SetValue(TrendIconProperty, value); }
        }
        public static readonly DependencyProperty TrendIconProperty = DependencyProperty.Register("TrendIcon", typeof(IconChar), typeof(DashboardCard));
    }
}