//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f2fca20b-a8b7-42d8-bbb9-8bd7b98f7a41
//        CLR Version:              4.0.30319.42000
//        Name:                     GrayscaleEffect
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                GrayscaleEffect
//
//        Created by Charley at 2017/4/11 9:47:42
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// An effect that turns the input into shades of a single color.
    /// </summary>
    public class GrayscaleEffect : ShaderEffect
    {
        /// <summary>
        /// Dependency property for Input
        /// </summary>
        public static readonly DependencyProperty InputProperty =
            RegisterPixelShaderSamplerProperty("Input", typeof(GrayscaleEffect), 0);

        /// <summary>
        /// Dependency property for FilterColor
        /// </summary>
        public static readonly DependencyProperty FilterColorProperty =
            DependencyProperty.Register("FilterColor", typeof(Color), typeof(GrayscaleEffect),
            new UIPropertyMetadata(Color.FromArgb(255, 255, 255, 255), PixelShaderConstantCallback(0)));

        /// <summary>
        /// Default constructor
        /// </summary>
        public GrayscaleEffect()
        {
            PixelShader pixelShader = new PixelShader();
            var prop = DesignerProperties.IsInDesignModeProperty;

            bool isInDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            try
            {
                if (!isInDesignMode) pixelShader.UriSource = new Uri("/NIRibbon;Component/Themes/Office2010/Effects/Grayscale.ps", UriKind.Relative);
            }
            catch { }
           
            PixelShader = pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(FilterColorProperty);
        }

        /// <summary>
        /// Impicit input
        /// </summary>
        public Brush Input
        {
            get
            {
                return ((Brush)(GetValue(InputProperty)));
            }
            set
            {
                SetValue(InputProperty, value);
            }
        }

        /// <summary>
        /// The color used to tint the input.
        /// </summary>
        public Color FilterColor
        {
            get
            {
                return ((Color)(GetValue(FilterColorProperty)));
            }
            set
            {
                SetValue(FilterColorProperty, value);
            }
        }
    }
}
