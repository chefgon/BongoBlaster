using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace BongoBlaster
{
    /// <summary>
    /// Interaction logic for BongoIllustration.xaml
    /// </summary>
    public partial class BongoIllustration : UserControl
    {
        #region Constructor
        public BongoIllustration()
        {
            InitializeComponent();
        }


        #endregion Constructor

        #region Private Properties
        #endregion Private Properties

        #region Public Properties
        public Boolean TopLeft
        {
            get
            {
                return (Boolean)this.GetValue(TopLeftProperty);
            }
            set
            {
                this.SetValue(TopLeftProperty, value);
            }
        }
        public static readonly DependencyProperty TopLeftProperty = DependencyProperty.Register("TopLeft", typeof(Boolean), typeof(BongoIllustration));

        public Boolean TopRight
        {
            get
            {
                return (Boolean)this.GetValue(TopRightProperty);
            }
            set
            {
                this.SetValue(TopRightProperty, value);
            }
        }
        public static readonly DependencyProperty TopRightProperty = DependencyProperty.Register("TopRight", typeof(Boolean), typeof(BongoIllustration));

        public Boolean BottomLeft
        {
            get
            {
                return (Boolean)this.GetValue(BottomLeftProperty);
            }
            set
            {
                this.SetValue(BottomLeftProperty, value);
            }
        }
        public static readonly DependencyProperty BottomLeftProperty = DependencyProperty.Register("BottomLeft", typeof(Boolean), typeof(BongoIllustration));

        public Boolean BottomRight
        {
            get
            {
                return (Boolean)this.GetValue(BottomRightProperty);
            }
            set
            {
                this.SetValue(BottomRightProperty, value);
            }
        }
        public static readonly DependencyProperty BottomRightProperty = DependencyProperty.Register("BottomRight", typeof(Boolean), typeof(BongoIllustration));

        public Boolean ClapSensor
        {
            get
            {
                return (Boolean)this.GetValue(ClapSensorProperty);
            }
            set
            {
                this.SetValue(ClapSensorProperty, value);
            }
        }
        public static readonly DependencyProperty ClapSensorProperty = DependencyProperty.Register("ClapSensor", typeof(Boolean), typeof(BongoIllustration));

        public Boolean StartButton
        {
            get
            {
                return (Boolean)this.GetValue(StartButtonProperty);
            }
            set
            {
                this.SetValue(StartButtonProperty, value);
            }
        }
        public static readonly DependencyProperty StartButtonProperty = DependencyProperty.Register("StartButton", typeof(Boolean), typeof(BongoIllustration));

        public Brush HighlightColor
        {
            get
            {
                return (Brush)this.GetValue(HighlightColorProperty);
            }
            set
            {
                this.SetValue(HighlightColorProperty, value);
            }
        }
        public static readonly DependencyProperty HighlightColorProperty = DependencyProperty.Register("HighlightColor", typeof(Brush), typeof(BongoIllustration));
        #endregion Public Properties

        #region Event Handlers
        
        #endregion Event Handlers
    }
}
