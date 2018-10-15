' The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

Imports Windows.Phone.UI.Input

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
''' <seealso cref="Windows.UI.Xaml.Controls.Page" />
''' <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
''' <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
Public NotInheritable Class MainPage
    Inherits Page

    ''' <summary>
    ''' Backs the pressed.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="BackPressedEventArgs"/> instance containing the event data.</param>
    Async Sub BackPressed(sender As Object, e As BackPressedEventArgs)
        'Handles any Back button presses.
        e.Handled = True
        If SSWV.CanGoBack Then
            SSWV.GoBack()
        Else
            Await displayMessageAsync("Quit " & AppName, "Are you sure you want to quit the app?", "")
        End If
        _G_ABOUT.Visibility = Visibility.Collapsed
        _G_COUNTRY.Visibility = Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' Handles the Click event of the _ACLOSE control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    Private Sub _ACLOSE_Click(sender As Object, e As RoutedEventArgs) Handles _ACLOSE.Click
        _G_ABOUT.Visibility = Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' Handles the Click event of the _CCLOSE control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    Private Sub _CCLOSE_Click(sender As Object, e As RoutedEventArgs) Handles _CCLOSE.Click
        _G_COUNTRY.Visibility = Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' Handles the Click event of the BACK control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    Private Async Sub BACK_Click(sender As Object, e As RoutedEventArgs) Handles BACK.Click
        If SSWV.CanGoBack Then
            SSWV.GoBack()
        Else
            Await displayMessageAsync("Quit " & AppName, "Are you sure you want to quit the app?", "")
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the HOME control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    Private Sub HOME_Click(sender As Object, e As RoutedEventArgs) Handles HOME.Click
        Dim selectedValue As Integer
        selectedValue = localSettings.Values("STORED_COUNTRY")
        SSWV.Source = New Uri("https://www.amazon." & CountryURL(selectedValue) & "/")
        iconRotation.Begin()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the hyperDev control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    Private Async Sub hyperDev_Click(sender As Object, e As RoutedEventArgs) Handles hyperDev.Click
        _G_ABOUT.Visibility = Visibility.Collapsed
        Dim DevURL = New Uri("https://github.com/CelestialDoom/")
        Await Windows.System.Launcher.LaunchUriAsync(DevURL)
    End Sub

    ''' <summary>
    ''' Handles the Click event of the hyperLogo control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    Private Async Sub hyperLogo_Click(sender As Object, e As RoutedEventArgs) Handles hyperLogo.Click
        _G_ABOUT.Visibility = Visibility.Collapsed
        Dim logoURL = New Uri("http://www.iconarchive.com/show/ios7-icons-by-icons8/Ecommerce-Shopping-Cart-Empty-icon.html")
        Await Windows.System.Launcher.LaunchUriAsync(logoURL)
    End Sub

    ''' <summary>
    ''' Handles the SelectionChanged event of the lstCountry control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
    Private Sub lstCountry_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstCountry.SelectionChanged
        Dim selectedindex As Integer
        Dim selectedValue As Integer
        selectedindex = lstCountry.SelectedIndex
        If selectedindex > -1 Then
            _G_COUNTRY.Visibility = Visibility.Collapsed
            Dim value As Object = localSettings.Values("STORED_COUNTRY")
            localSettings.Values("STORED_COUNTRY") = selectedindex
            selectedValue = localSettings.Values("STORED_COUNTRY")
            SSWV.Source = New Uri("https://www.amazon." & CountryURL(selectedValue) & "/")
        End If
    End Sub

    ''' <summary>
    ''' Handles the Loaded event of the MainPage control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        IsSideBarOpen = False
        SB.Visibility = Visibility.Collapsed
        lstCountry.ItemsSource = LoadCountries()
        _G_COUNTRY.Visibility = Visibility.Collapsed
        AddHandler HardwareButtons.BackPressed, AddressOf BackPressed
        If Stored_ID Is Nothing Then
            'localSettings.Values("STORED_COUNTRY") = "0"
            _G_COUNTRY.Visibility = Visibility.Visible
        Else
            CountryValue = Stored_ID
        End If
        Dim value As Object = localSettings.Values("STORED_COUNTRY")
        URLBuilder = "https://www.amazon." & CountryURL(value) & "/"
        iconRotation.Begin()
        SSWV.Source = New Uri("https://www.amazon." & CountryURL(value) & "/")
    End Sub

    Private Async Sub PrivacyURL_Click(sender As Object, e As RoutedEventArgs) Handles PrivacyURL.Click
        _G_ABOUT.Visibility = Visibility.Collapsed
        Dim logoURL = New Uri("https://github.com/CelestialDoom/SlimShopper/blob/master/Privacy-Policy.md")
        Await Windows.System.Launcher.LaunchUriAsync(logoURL)
    End Sub

    ''' <summary>
    ''' Handles the LoadCompleted event of the SSWV control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
    Private Sub SSWV_LoadCompleted(sender As Object, e As NavigationEventArgs) Handles SSWV.LoadCompleted
        iconRotation.Stop()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the TOP control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    Private Async Sub TOP_Click(sender As Object, e As RoutedEventArgs) Handles TOP.Click
        Dim ScrollToTopString = "var int = setInterval(function(){window.scrollBy(0, -36); if( window.pageYOffset === 0 ) clearInterval(int); }, 0.0);"
        Await SSWV.InvokeScriptAsync("eval", New String() {ScrollToTopString})
    End Sub

    Private Sub _MENU_Click(sender As Object, e As RoutedEventArgs) Handles _MENU.Click
        If IsSideBarOpen Then
            IsSideBarOpen = False
            SB.Visibility = Visibility.Collapsed
        Else
            IsSideBarOpen = True
            SB.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub CloseSB()
        SB.Visibility = Visibility.Collapsed
        IsSideBarOpen = False
    End Sub

    Private Sub _COUNTRY_Click(sender As Object, e As RoutedEventArgs) Handles _COUNTRY.Click
        CloseSB()
        _G_COUNTRY.Visibility = Visibility.Visible
    End Sub

    Private Sub _MYACCOUNT_Click(sender As Object, e As RoutedEventArgs) Handles _MYACCOUNT.Click
        CloseSB()
        Dim selectedValue As Integer
        selectedValue = localSettings.Values("STORED_COUNTRY")
        SSWV.Source = New Uri("https://www.amazon." & CountryURL(selectedValue) & "/gp/css/homepage.html/ref=nav_youraccount_ya")
    End Sub

    Private Sub _MYORDERS_Click(sender As Object, e As RoutedEventArgs) Handles _MYORDERS.Click
        CloseSB()
        Dim selectedValue As Integer
        selectedValue = localSettings.Values("STORED_COUNTRY")
        SSWV.Source = New Uri("https://www.amazon." & CountryURL(selectedValue) & "/gp/css/order-history/ref=nav_youraccount_order")
    End Sub

    Private Sub _ABOUTAPP_Click(sender As Object, e As RoutedEventArgs) Handles _ABOUTAPP.Click
        CloseSB()
        PivotSettingsAbout.SelectedIndex = 0
        Dim number As PackageVersion = Package.Current.Id.Version
        version.Text = String.Format(" {0}.{1}.{2}" & vbCrLf, number.Major, number.Minor, number.Build)
        abouttext.Text = AboutInfo
        privacy.Text = PrivacyInfo
        ScrollViewAbout.ChangeView(Nothing, 0, Nothing, True)
        ScrollViewPrivacy.ChangeView(Nothing, 0, Nothing, True)
        _G_ABOUT.Visibility = Visibility.Visible
    End Sub

    Private Sub _QUITAPP_Click(sender As Object, e As RoutedEventArgs) Handles _QUITAPP.Click
        CloseSB()
        Application.Current.Exit()
    End Sub

    Private Sub txtC_CHOICE_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles txtC_CHOICE.Tapped
        CloseSB()
        _G_COUNTRY.Visibility = Visibility.Visible
    End Sub

    Private Sub txtMy_Account_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles txtMy_Account.Tapped
        CloseSB()
        Dim selectedValue As Integer
        selectedValue = localSettings.Values("STORED_COUNTRY")
        SSWV.Source = New Uri("https://www.amazon." & CountryURL(selectedValue) & "/gp/css/homepage.html/ref=nav_youraccount_ya")
    End Sub

    Private Sub txtMy_Orders_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles txtMy_Orders.Tapped
        CloseSB()
        Dim selectedValue As Integer
        selectedValue = localSettings.Values("STORED_COUNTRY")
        SSWV.Source = New Uri("https://www.amazon." & CountryURL(selectedValue) & "/gp/css/order-history/ref=nav_youraccount_order")
    End Sub

    Private Sub txtAboutApp_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles txtAboutApp.Tapped
        CloseSB()
        PivotSettingsAbout.SelectedIndex = 0
        Dim number As PackageVersion = Package.Current.Id.Version
        version.Text = String.Format(" {0}.{1}.{2}" & vbCrLf, number.Major, number.Minor, number.Build)
        abouttext.Text = AboutInfo
        privacy.Text = PrivacyInfo
        ScrollViewAbout.ChangeView(Nothing, 0, Nothing, True)
        ScrollViewPrivacy.ChangeView(Nothing, 0, Nothing, True)
        _G_ABOUT.Visibility = Visibility.Visible
    End Sub

    Private Sub txtQuitApp_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles txtQuitApp.Tapped
        CloseSB()
        Application.Current.Exit()
    End Sub

End Class