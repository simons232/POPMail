﻿

#pragma checksum "C:\Users\Simons\documents\visual studio 2012\Projects\POPMail\POPMail\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C8BEFC8174D66B330DDCCD9996466F48"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POPMail
{
    partial class MainPage : global::POPMail.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 100 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.MailTestPageButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 97 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.PasswordBox)(target)).PasswordChanged += this.passwordInput_PasswordChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 93 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.nameInput_TextChanged;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 89 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.serverInput_TextChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 80 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


